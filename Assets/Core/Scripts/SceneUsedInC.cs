using System;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;

namespace ZeroZeroGames.Ecs.Modules.Unsorted.Components
{
    [Serializable]
    public struct SceneUsedInC : IComponent
    {
        [StaticEcsEditorTableValue] public SceneUsedInType val;

        public enum SceneUsedInType
        {
            Bootstrap,
            Meta,
            Core
        }

        public SceneUsedInC(SceneUsedInType val)
        {
            this.val = val;
        }
    }
}