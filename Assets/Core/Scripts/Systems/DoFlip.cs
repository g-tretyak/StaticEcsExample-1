using Core.Scripts.MasksTest;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoFlip : IUpdateSystem
    {
        public void Update()
        {
            MaskNone<JumpingHorizontally, Attacking, Dashing, Recoiling, Stunned> withoutMasks = default;
            W.QueryComponents.With(withoutMasks).For((ref MoveInputC input, ref FlipC flip) =>
            {
                flip.Set(input.newVal.x);
            });

            W.QueryComponents.For((W.Entity e, ref FlipC flip, ref VisualsC visuals) =>
            {
                var scale = visuals.spriteRend.transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                scale.x *= flip.lastNonZeroX;
                visuals.spriteRend.transform.localScale = scale;
            });
        }
    }
}