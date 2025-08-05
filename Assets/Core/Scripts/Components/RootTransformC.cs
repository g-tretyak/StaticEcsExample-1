using System;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;
using UnityEngine;

namespace ZeroZeroGames.Ecs.Modules.Shared.Components
{
    [Serializable]
    public struct RootTransformC : IComponent
    {
        public Transform val;

        [StaticEcsEditorTableValue] public Vector3 Pos => val.position;

        public RootTransformC(Transform val)
        {
            this.val = val;
        }
    }
}