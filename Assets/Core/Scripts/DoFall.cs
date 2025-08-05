using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoFall : IUpdateSystem
    {
        public void Update()
        {
            W.QueryComponents.For(
                (W.Entity e, ref FallC fall, ref RootRigidbody2DC rb) =>
                {
                    rb.val.gravityScale = e.HasDisabledAllOf<FallC>() ? 0f : 1f;
                }, components: ComponentStatus.Any);
        }
    }
}