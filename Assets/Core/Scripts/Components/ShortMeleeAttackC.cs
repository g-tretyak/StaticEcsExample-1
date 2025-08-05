using System;
using Core.Scripts.Plain;
using FFS.Libraries.StaticEcs;

namespace Core.Scripts
{
    [Serializable]
    public struct ShortMeleeAttackC : IComponent
    {
        public MeleeModule mod;

        public ShortMeleeAttackC(float val, float cooldownMaxVal, float usingVal)
        {
            mod = new MeleeModule(val, cooldownMaxVal, usingVal);
        }
    }
}