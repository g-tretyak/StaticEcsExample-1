using Client.Scripts;
using Core.Scripts.MasksTest;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;
using ZeroZeroGames.Ecs.Modules.Unsorted.Tags;

namespace Core.Scripts
{
    public struct DoMeleeAttack : IInitSystem, IUpdateSystem, IDestroySystem
    {
        private static void CheckSkipOnStun(W.Entity ent, TimerModule mod, IAttack attack)
        {
            if (ent.HasAllOfMasks<Stunned>())
            {
                mod.SkipTimer();
                attack.DoCleanup(ent);
            }
        }

        public void Update()
        {
            ReceiveHeroInput();

            foreach (var ev in _eventReceiver)
            {
                if (ev.Value.ent.TryUnpack(out W.Entity ent) == false) continue;
                if (ent.HasAllOf<RootRigidbody2DC>() == false) continue;
                if (ent.HasAllOf<MoveInputC>() == false) continue;
                if (ent.HasAllOf<TeamC>() == false) continue;

                if (ent.HasAnyOfMasks<Attacking, Stunned>()) continue;

                ref var power = ref ev.Value.attack;

                if (power.CanUse() == false) continue;

                power.OnStart(ent);
            }

            W.QueryComponents
                .For((W.Entity ent, ref ShortMeleeAttackC power) =>
                {
                    CheckSkipOnStun(ent, power.mod.usingMod, power.mod);
                    power.mod.OnTick(ent);
                }, components: ComponentStatus.Any);

            W.QueryComponents
                .For((W.Entity ent, ref ChargeMeleeAttackC power) =>
                {
                    CheckSkipOnStun(ent, power.mod.usingMod, power.mod);
                    power.mod.OnTick(ent);
                }, components: ComponentStatus.Any);

            W.QueryComponents
                .For((W.Entity _, ref ComboC combo) => { combo.timer.TickTimer(); });

            W.QueryComponents
                .For((W.Entity _, ref ReceiveDamageBlockC dmgBlock) => { dmgBlock.timer.TickTimer(); });

            W.QueryComponents
                .For((W.Entity e, ref KnockbackPowerC knockback) =>
                {
                    knockback.cooldownMod.TickTimer();
                    knockback.usingMod.TickTimer();
                    if (knockback.usingMod.OnTimer == false) e.TryDeleteMask<Stunned>();
                }, components: ComponentStatus.Any);
        }

        public static int GetEntDamage(W.Entity ent)
        {
            if (ent.HasAllOf<DamageC>() == false) return 0;
            ref var damage = ref ent.Ref<DamageC>();
            if (ent.HasAllOf<ComboC>() == false) return damage.val;
            ref var combo = ref ent.Ref<ComboC>();
            if (combo.timer.OnTimer == false) combo.curAttacks = 0;

            combo.curAttacks++;
            combo.timer.SetTimer();

            if (combo.curAttacks < combo.maxAttacks) return damage.val;

            combo.curAttacks = 0;
            combo.timer.timer = 0f;
            return damage.val + combo.damageAddOnCombo;
        }

        private EventReceiver<WT, AttackRequested> _eventReceiver;

        private static bool IsFrontFaceUndamageable(W.Entity defenderEnt, RootRigidbody2DC defenderRb,
            Vector2 attackerPos, float attackerDir)
        {
            if (defenderEnt.HasAllOfMasks<Attacking>() == false) return false;
            ref var flip = ref defenderEnt.Ref<FlipC>();

            var a = new Vector2(defenderRb.val.position.x, defenderRb.val.position.x + flip.lastNonZeroX);
            var b = new Vector2(attackerPos.x, attackerPos.x + attackerDir);

            var dotProduct = Vector2.Dot(a, b);

            return dotProduct switch
            {
                > 0 => false,
                _ => true
            };
        }

        public static bool ProcessAttack(TeamC.TeamType teamType, Vector2 pos,
            float dir, int damage)
        {
            var res = false;

            pos.x += Cxt.S.attackOffset * dir;

            EventHelpers.SpawnVfx(Cxt.S.debugAttackVfxPrefab, pos, Cxt.S.debugAttackVfxLifeDur);

            W.QueryComponents
                .For((W.Entity ent, ref HealthC health, ref RootRigidbody2DC rb, ref TeamC team) =>
                {
                    if (ent.HasAllOf<ReceiveDamageBlockC>() && ent.Ref<ReceiveDamageBlockC>().timer.OnTimer) return;
                    if (GridHelpers.IsCloseEnoughTo(pos,
                            rb.val.position, Cxt.S.attackRadius) == false) return;
                    if (teamType == team.team) return;
                    if (IsFrontFaceUndamageable(ent, rb, pos, dir)) return;
                    res = true;

                    health.val -= damage;

                    //TODO event helpers
                    W.Events.Send(new DamageDealt(ent.Gid(), damage, pos));

                    //TODO вынести отсюда
                    if (health.val <= 0)
                    {
                        ent.SetTag<IsForDeletion>();

                        if (team.team == TeamC.TeamType.Enemy)
                        {
                            Cxt.R.Core.RedLootCur += Cxt.S.enemyLootRed;
                            Cxt.R.Core.GreenLootCur += Cxt.S.enemyLootGreen;
                            Cxt.R.Core.BlueLootCur += Cxt.S.enemyLootBlue;
                        }
                        else if (team.team == TeamC.TeamType.Player)
                        {
                            FulfillStartGame.DropPuzzlePiece(Cxt.R.Core.CurPuzzlePiece);
                            IncreaseAllLootBy(Cxt.S.lootDropOnDefeatPercent / 100f * -1f);
                            PausePanelProvider.GoBackToHub();
                        }
                    }

                    if (team.team == TeamC.TeamType.Player)
                        ent.Put(new ReceiveDamageBlockC(Cxt.S.playerDamageReceiveBlockDur));

                    if (ent.HasAllOf<KnockbackPowerC>())
                    {
                        ref var knockback = ref ent.Ref<KnockbackPowerC>();
                        knockback.cooldownMod.SetTimer();
                        knockback.usingMod.SetTimer();
                        rb.val.linearVelocityX = 0f;
                        rb.val.linearVelocityY = 0f;
                        rb.val.AddForceX(knockback.val * dir, ForceMode2D.Impulse);

                        ent.SetMask<Stunned>();
                    }
                });

            return res;
        }

        public static void IncreaseAllLootBy(float mod)
        {
            Cxt.R.Core.RedLootCur += Mathf.RoundToInt(Cxt.R.Core.RedLootCur * mod);
            Cxt.R.Core.GreenLootCur += Mathf.RoundToInt(Cxt.R.Core.GreenLootCur * mod);
            Cxt.R.Core.BlueLootCur += Mathf.RoundToInt(Cxt.R.Core.BlueLootCur * mod);
        }

        private void ReceiveHeroInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Cxt.R.Core.MainHero.TryUnpack(out W.Entity e))
                W.Events.Send(new AttackRequested(Cxt.R.Core.MainHero, e.Ref<ShortMeleeAttackC>().mod));
        }

        public void Init()
        {
            StaticEcsHelpers.RegisterEventReceiver(out _eventReceiver);
        }

        public void Destroy()
        {
            StaticEcsHelpers.DeleteEventReceiver(ref _eventReceiver);
        }
    }
}