using Core.Scripts.MasksTest;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoRecoil<T1, T2> : IInitSystem, IUpdateSystem, IDestroySystem
        where T1 : struct, IDash, IComponent where T2 : struct, IMask
    {
        private readonly float _mod;
        private readonly bool _isOnlyGround;

        public DoRecoil(bool pos, bool isOnlyGround)
        {
            _eventReceiver = default;
            _mod = pos ? 1f : -1f;
            _isOnlyGround = isOnlyGround;
        }

        public void Update()
        {
            ReceiveHeroInput();

            foreach (var ev in _eventReceiver)
            {
                if (ev.Value.ent.TryUnpack(out W.Entity ent) == false) continue;

                if (ent.HasDisabledAllOf<T1>()) continue;
                if (ent.HasAllOf<RootRigidbody2DC, FlipC, DashingChargesC>() == false) continue;

                ref var power = ref ent.Ref<T1>();
                ref var rb = ref ent.Ref<RootRigidbody2DC>();
                ref var flip = ref ent.Ref<FlipC>();
                ref var charges = ref ent.Ref<DashingChargesC>();

                if (DoJump.IsTouchingGround(rb.val.transform) != _isOnlyGround) continue;
                if (power.UsingMod.OnTimer || power.CooldownMod.OnTimer) continue;
                if (ent.HasAnyOfMasks<T2, Attacking, Stunned>()) continue;
                if (charges.IsEnough(power.Cost) == false) continue;

                charges.Increase(power.Cost * -1);
                rb.val.linearVelocityX = 0f;
                rb.val.AddForceX(power.Value * flip.lastNonZeroX * _mod, ForceMode2D.Impulse);
                power.CooldownMod.SetTimer();
                power.UsingMod.SetTimer();
                ent.SetMask<T2>();
            }

            W.QueryComponents
                .For((W.Entity e, ref T1 power) =>
                {
                    power.CooldownMod.TickTimer();
                    power.UsingMod.TickTimer();

                    if (power.UsingMod.OnTimer == false) e.TryDeleteMask<T2>();
                }, components: ComponentStatus.Any);
        }

        private EventReceiver<WT, RecoilRequested> _eventReceiver;

        private void ReceiveHeroInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && Cxt.R.Core.MainHero.TryUnpack(out W.Entity _))
                W.Events.Send(new RecoilRequested(Cxt.R.Core.MainHero));
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