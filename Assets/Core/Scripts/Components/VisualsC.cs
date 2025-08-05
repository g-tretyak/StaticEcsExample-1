using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace Core.Scripts
{
    public struct VisualsC : IComponent
    {
        public SpriteRenderer spriteRend;

        public VisualsC(SpriteRenderer spriteRend)
        {
            this.spriteRend = spriteRend;
        }
    }
}