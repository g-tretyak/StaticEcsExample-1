using System;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;
using UnityEngine;

namespace ZeroZeroGames.Ecs.Modules.Shared.Components
{
    [Serializable]
    public struct RootRigidbody2DC : IComponent
    {
        public Rigidbody2D val;

        [StaticEcsEditorTableValue] public Vector2 Velocity => val.linearVelocity;

        public RootRigidbody2DC(Rigidbody2D val)
        {
            this.val = val;
        }
    }
}