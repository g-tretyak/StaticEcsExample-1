using System;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;

namespace ZeroZeroGames.Ecs.Modules.Shared.Events
{
    [Serializable]
    public struct AddToRootCanvasRequested : IEvent
    {
        public CanvasProvider target;
        public int sortingOrder;

        [StaticEcsEditorTableValue] public string Info => $"{target?.name}_({sortingOrder})";

        public AddToRootCanvasRequested(CanvasProvider target, int sortingOrder)
        {
            this.target = target;
            this.sortingOrder = sortingOrder;
        }
    }
}