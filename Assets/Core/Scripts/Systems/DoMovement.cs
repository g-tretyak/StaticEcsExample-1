using Core.Scripts.MasksTest;
using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Move.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoMovement : IUpdateSystem
    {
        public void Update()
        {
            W.QueryComponents
                .For((W.Entity e, ref MoveSpeedC speed, ref RootRigidbody2DC rb,
                    ref RootTransformC transform) =>
                {
                    if (e.HasAnyOfMasks<Jumping, JumpingHorizontally, Attacking>()) return;
                    if (e.HasAnyOfMasks<Dashing, Recoiling, Stunned>()) return;
                    rb.val.linearVelocityX = speed.val * e.Ref<MoveInputC>().newVal.x;
                });
        }
    }
}