using System;
using FFS.Libraries.StaticEcs;

namespace Core.Scripts
{
    public struct Triggerable : IComponent
    {
        public Action actionOnInteract;

        public Triggerable(Action actionOnInteract)
        {
            this.actionOnInteract = actionOnInteract;
        }
    }
}