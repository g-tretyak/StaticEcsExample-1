using Core.Scripts.MasksTest;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public class BasicEnemyAi : IEnemyAI
    {
        public void Tick(W.Entity e)
        {
            if (e.HasAnyOfMasks<Attacking, Dashing, Jumping>()) return;
            if (e.HasAnyOfMasks<JumpingHorizontally, Recoiling, Stunned>()) return;

            ref var input = ref e.Ref<MoveInputC>();
            input.SetVal(Vector2Int.zero);

            if (Cxt.R.Core.MainHero.TryUnpack(out W.Entity heroE) == false) return;
            var heroPos = heroE.Ref<RootTransformC>().Pos;
            var enemyPos = e.Ref<RootTransformC>().Pos;
            if (GridHelpers.IsCloseEnoughTo(enemyPos.x, heroPos.x,
                    Cxt.S.enemyChaseDist) == false)
            {
                ref var wander = ref e.Ref<WanderC>();
                wander.duration -= Time.deltaTime;
                if (wander.duration <= 0)
                {
                    wander.direction *= -1;
                    wander.duration = Random.Range(Cxt.S.minWander, Cxt.S.maxWander);
                }


                input.SetVal(GridHelpers.ConvertDirectionToInt(new Vector2(wander.direction, 0)));
                return;
            }

            var dir = Mathf.Sign(heroPos.x - enemyPos.x);

            if (GridHelpers.IsCloseEnoughTo(enemyPos.x, heroPos.x,
                    Cxt.S.enemyStopChaseDist))
            {
                e.Ref<FlipC>().Set((int)dir);
                dir = 0f;
            }

            input.SetVal(GridHelpers.ConvertDirectionToInt(new Vector2(dir, 0)));

            if (GridHelpers.IsCloseEnoughTo(enemyPos.x, heroPos.x,
                    Cxt.S.enemyAttackDist) == false)
                return;

            IAttack a = null;
            if (Cxt.S.chargeAttackChance / 100f >= Random.value && e.Ref<ChargeMeleeAttackC>().mod.CanUse())
                a = e.Ref<ChargeMeleeAttackC>().mod;
            else if (e.Ref<ShortMeleeAttackC>().mod.CanUse()) a = e.Ref<ShortMeleeAttackC>().mod;

            if (a != null) W.Events.Send(new AttackRequested(e.Gid(), a));
        }
    }
}