using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Events;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace ZeroZeroGames.Ecs.Modules.Unsorted.Systems
{
    public struct FulfillLogToConsole : IInitSystem, IUpdateSystem, IDestroySystem
    {
        private EventReceiver<WT, LogRequested> _eventReceiver;

        public void Init()
        {
            StaticEcsHelpers.RegisterEventReceiver(out _eventReceiver);
        }

        public void Update()
        {
            foreach (var logRequestedEventReceiver in _eventReceiver)
                Debug.Log($"ЛОГ: {logRequestedEventReceiver.Value.message}");
        }

        public void Destroy()
        {
            StaticEcsHelpers.DeleteEventReceiver(ref _eventReceiver);
        }
    }
}