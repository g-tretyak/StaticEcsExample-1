using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.World;
using ZeroZeroGames.Ecs.Modules.Unsorted.Components;

namespace ZeroZeroGames.Ecs.Modules.Shared.Helpers
{
    public static class EntityHelpers
    {
        public static W.Entity CreateEntity<T>(T v) where T : struct, IComponent
        {
            var e = W.Entity.New(v);
            e.Add(new SceneUsedInC(Cxt.R.Bootstrap.CurrentScene));
            return e;
        }

        public static W.Entity CreateEmptyEntity()
        {
            return W.Entity.New();
        }
    }
}