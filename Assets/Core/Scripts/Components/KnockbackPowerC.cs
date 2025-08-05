using System;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;

namespace Core.Scripts
{
    [Serializable]
    public struct KnockbackPowerC : IComponent
    {
        [StaticEcsEditorTableValue] public float val;

        public TimerModule cooldownMod;
        public TimerModule usingMod;

        public KnockbackPowerC(float val, float cooldownMaxVal, float usingVal)
        {
            this.val = val;
            cooldownMod = new TimerModule(cooldownMaxVal);
            usingMod = new TimerModule(usingVal);
        }
    }
}