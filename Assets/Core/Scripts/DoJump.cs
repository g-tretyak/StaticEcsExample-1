using Core.Scripts.MasksTest;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoJump : IUpdateSystem
    {
        public void Update()
        {
            W.QueryComponents.For((W.Entity e,
                ref JumpPowerC power, ref RootTransformC transform) =>
            {
                power.cooldownMod.TickTimer();
                if (power.cooldownMod.OnTimer) return;
                if (IsTouchingGround(transform.val) == false) return;
                power.usingMod.SkipTimer();
                e.TryDeleteMask<Jumping>();
                e.TryDeleteMask<JumpingHorizontally>();
            }, components: ComponentStatus.Any);

            W.QueryComponents
                .For((W.Entity e, ref JumpPowerC power, ref RootRigidbody2DC rb, ref MoveInputC input,
                    ref RootTransformC transform) =>
                {
                    if (NeedsJump(transform.val) == false) return;
                    if (e.HasAnyOfMasks<Jumping, JumpingHorizontally>()) return;
                    if (e.HasAnyOfMasks<Attacking, Stunned>()) return;
                    rb.val.linearVelocityY = 0f;
                    rb.val.AddForceY(power.val, ForceMode2D.Impulse);
                    power.cooldownMod.SetTimer();
                    power.usingMod.SetTimer();

                    e.SetMask<Jumping>();

                    if (input.newVal.x != 0) e.SetMask<JumpingHorizontally>();
                });
        }

        private static bool NeedsJump(Transform tr)
        {
            if (Input.GetKey(KeyCode.Space) == false) return false;
            if (IsTouchingGround(tr) == false) return false;

            return true;
        }

        public static bool IsTouchingGround(Transform transform)
        {
            var pos = transform.position + new Vector3(0, -0.25f, 0);
            var size = new Vector3(0.4f, 0.05f, 1f);
            var result = Physics2D.BoxCast(pos, size, 0f,
                Vector2.zero, 0f, Cxt.S.groundLayerMask).collider != null;

            return result;
        }
    }
}