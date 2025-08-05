using System;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;

namespace Core.Scripts
{
    [Serializable]
    public struct DashPowerC : IComponent, IDash
    {
        [StaticEcsEditorTableValue] public float val;

        public TimerModule cooldownMod;
        public TimerModule usingMod;
        public int cost;

        public DashPowerC(float val, float cooldownMaxVal, float usingVal, int cost)
        {
            this.val = val;
            cooldownMod = new TimerModule(cooldownMaxVal);
            usingMod = new TimerModule(usingVal);
            this.cost = cost;
        }

        public float Value => val;
        public TimerModule UsingMod => usingMod;
        public TimerModule CooldownMod => cooldownMod;
        public int Cost => cost;
    }
}