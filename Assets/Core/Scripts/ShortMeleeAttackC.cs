using System;
using Core.Scripts.MasksTest;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    [Serializable]
    public class MeleeModule : IAttack
    {
        public float val;

        public TimerModule cooldownMod;
        public TimerModule usingMod;

        public MeleeModule(float val, float cooldownMaxVal, float usingVal)
        {
            this.val = val;
            cooldownMod = new TimerModule(cooldownMaxVal);
            usingMod = new TimerModule(usingVal);
        }

        public void OnTick(W.Entity ent)
        {
            cooldownMod.TickTimer();

            var wasUsing = usingMod.OnTimer;

            usingMod.TickTimer();

            if (wasUsing && usingMod.OnTimer == false)
            {
                if (ent.HasAllOf<RootRigidbody2DC>() == false) return;
                if (ent.HasAllOf<FlipC>() == false) return;
                if (ent.HasAllOf<TeamC>() == false) return;

                ref var rb = ref ent.Ref<RootRigidbody2DC>();
                ref var flip = ref ent.Ref<FlipC>();
                ref var team = ref ent.Ref<TeamC>();

                var res = DoMeleeAttack.ProcessAttack(team.team, rb.val.position,
                    flip.lastNonZeroX, DoMeleeAttack.GetEntDamage(ent));
                if (res)
                    EventHelpers.RequestLog("Hit success");
                DoCleanup(ent);
            }
        }

        public void DoCleanup(W.Entity ent)
        {
            ent.TryDeleteMask<Attacking>();
        }

        public void OnStart(W.Entity e)
        {
            ref var rb = ref e.Ref<RootRigidbody2DC>();
            ref var flip = ref e.Ref<FlipC>();
            rb.val.linearVelocityX = 0f;
            rb.val.AddForceX(val * flip.lastNonZeroX, ForceMode2D.Impulse);
            cooldownMod.SetTimer();
            usingMod.SetTimer();

            e.SetMask<Attacking>();
        }

        public bool CanUse()
        {
            return cooldownMod.OnTimer == false &&
                   usingMod.OnTimer == false;
        }
    }

    [Serializable]
    public struct ShortMeleeAttackC : IComponent
    {
        public MeleeModule mod;

        public ShortMeleeAttackC(float val, float cooldownMaxVal, float usingVal)
        {
            mod = new MeleeModule(val, cooldownMaxVal, usingVal);
        }
    }
}