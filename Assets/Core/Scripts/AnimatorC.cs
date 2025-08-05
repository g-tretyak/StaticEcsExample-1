using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace Core.Scripts
{
    public struct AnimatorC : IComponent
    {
        public Animator animator;

        public AnimatorC(Animator animator)
        {
            this.animator = animator;
        }
    }
}