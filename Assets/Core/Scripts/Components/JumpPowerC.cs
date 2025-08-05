using System;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;

namespace Core.Scripts
{
    [Serializable]
    public struct JumpPowerC : IComponent
    {
        [StaticEcsEditorTableValue] public float val;

        public TimerModule cooldownMod;
        public TimerModule usingMod;

        private const float Cooldown = 0.5f;

        public JumpPowerC(float val)
        {
            this.val = val;
            cooldownMod = new TimerModule(Cooldown);
            usingMod = new TimerModule(Cooldown);
        }
    }
}