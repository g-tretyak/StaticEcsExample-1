using System;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;

namespace ZeroZeroGames.Ecs.Modules.Move.Components
{
    [Serializable]
    public struct MoveSpeedC : IComponent
    {
        [StaticEcsEditorTableValue] public float val;

        public MoveSpeedC(float val)
        {
            this.val = val;
        }
    }
}