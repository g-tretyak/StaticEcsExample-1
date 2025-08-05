using System;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace ZeroZeroGames.Ecs.Modules.Shared.Components
{
    [Serializable]
    public struct MainColliderC2D : IComponent
    {
        public Collider2D val;

        public MainColliderC2D(Collider2D val)
        {
            this.val = val;
        }
    }

    public static class MainColliderCExtension
    {
        public static void With(ref this MainColliderC2D comp,
            Collider2D val)
        {
            comp.val = val;
        }
    }
}