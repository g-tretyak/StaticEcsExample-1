using System;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;

namespace ZeroZeroGames.Ecs.Modules.Shared.Events
{
    [Serializable]
    public struct LogRequested : IEvent
    {
        [StaticEcsEditorTableValue] public string message;
    }
}