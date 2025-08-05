using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace ZeroZeroGames.Ecs.Modules.Shared.Helpers
{
    public static class StaticEcsHelpers
    {
        public static void RegisterEventReceiver<T>(out
            EventReceiver<WT, T> v) where T : struct, IEvent
        {
            v = W.Events.RegisterEventReceiver<T>();
        }

        public static void DeleteEventReceiver<T>(
            ref EventReceiver<WT, T> v) where T : struct, IEvent
        {
            W.Events.DeleteEventReceiver(ref v);
        }

        public static void DebugEntity(W.Entity entity)
        {
            EventHelpers.RequestLog($"Дебаг сущности: {entity.PrettyString}");
        }

        public static bool TrySetStatus<T>(W.Entity e, bool status) where T : struct, IComponent
        {
            if (e.HasAllOf<T>() == false) return false;
            SetStatus<T>(e, status);
            return true;
        }

        public static void SetStatus<T>(W.Entity e, bool status) where T : struct, IComponent
        {
            if (status)
            {
                e.Enable<T>();
                return;
            }

            e.Disable<T>();
        }
    }
}