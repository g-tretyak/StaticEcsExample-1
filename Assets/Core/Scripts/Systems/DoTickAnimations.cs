using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.World;
using ZeroZeroGames.Ecs.Modules.Unsorted.Components;

namespace ZeroZeroGames.Core.Scripts.Ecs.Modules.Shared.Unsorted.Systems
{
    public struct DoTickAnimations : IUpdateSystem
    {
        public void Update()
        {
            W.QueryComponents
                .For((W.Entity _, ref AnimationC anim) => { anim.Tick(); });
        }
    }
}