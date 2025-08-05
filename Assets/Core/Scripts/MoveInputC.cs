using System;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace ZeroZeroGames.Ecs.Modules.Shared.Components
{
    [Serializable]
    public struct MoveInputC : IComponent
    {
        public Vector2Int oldVal;

        public Vector2Int newVal;

        public void SetVal(Vector2Int axis)
        {
            if (axis == newVal) return;
            oldVal = newVal;
            newVal = axis;
        }
    }
}