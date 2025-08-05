using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;
using ZeroZeroGames.Ecs.Modules.Unsorted.Tags;

namespace ZeroZeroGames.Ecs.Modules.Unsorted.Systems
{
    public struct DoDeleteIfTagged : IUpdateSystem
    {
        public void Update()
        {
            foreach (var entity in W.QueryEntities.For<TagAll<IsForDeletion>>())
            {
                if (entity.HasAllOf<RootTransformC>())
                {
                    ref var rootTr = ref entity.Ref<RootTransformC>();
                    if (rootTr.val != null && rootTr.val.gameObject != null)
                    {
                        EventHelpers.RequestLog($"Уничтожение объекта: '{rootTr.val.gameObject}'");
                        PrefabHelpers.DestroyInstantiatedFromPrefab(rootTr.val.gameObject);
                    }
                }

                EventHelpers.RequestLog($"Уничтожение сущности: '{entity.PrettyString}'");
                entity.Destroy();
            }
        }
    }
}