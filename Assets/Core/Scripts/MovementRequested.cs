using System;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;

namespace ZeroZeroGames.Ecs.Modules.Shared.Events
{
    [Serializable]
    public struct MovementRequested : IEvent
    {
        public EntityGID target;

        [StaticEcsEditorTableValue] public float speed;
    }
}