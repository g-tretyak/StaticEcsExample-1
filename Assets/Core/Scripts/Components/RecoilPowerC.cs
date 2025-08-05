using System;
using System.Collections.Generic;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;
using UnityEngine;

namespace Core.Scripts
{
    [Serializable]
    public struct RecoilPowerC : IComponent, IDash
    {
        [StaticEcsEditorTableValue] public float val;

        public TimerModule cooldownMod;
        public TimerModule usingMod;
        public int cost;

        public RecoilPowerC(float val, float cooldownMaxVal, float usingVal, int cost)
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