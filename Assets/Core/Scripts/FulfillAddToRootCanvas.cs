using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Events;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace ZeroZeroGames.Ecs.Modules.StartMeta.Systems
{
    public struct FulfillAddToRootCanvas : IInitSystem, IUpdateSystem, IDestroySystem
    {
        private EventReceiver<WT, AddToRootCanvasRequested> _eventReceiver;

        public void Init()
        {
            StaticEcsHelpers.RegisterEventReceiver(out _eventReceiver);
        }

        public void Update()
        {
            foreach (var ev in _eventReceiver) AddToRootCanvas(ev.Value);
        }

        private static void AddToRootCanvas(AddToRootCanvasRequested eventValue)
        {
            var c = eventValue.target;
            if (c == null) return;

            var rect = c.CanvasRect;

            rect.position = Vector3.zero;

            var rootCanvas = Cxt.R.Bootstrap.RootCanvas;
            rect.SetParent(rootCanvas.transform);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            c.Canvas.overrideSorting = true;
            c.Canvas.sortingOrder = eventValue.sortingOrder;

            c.Canvas.transform.localScale = Vector3.one;

            EventHelpers.RequestLog("Выполнен запрос на добавление канваса к рут-канвасу. " +
                                    $"Слой: {c.Canvas.sortingOrder}");
        }

        public void Destroy()
        {
            StaticEcsHelpers.DeleteEventReceiver(ref _eventReceiver);
        }
    }
}