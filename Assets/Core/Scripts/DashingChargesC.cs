using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace Core.Scripts
{
    public struct DashingChargesC : IComponent
    {
        public int val;
        public int max;
        public TimerModule timer;
        public int add;

        public DashingChargesC(int max, int addPerRestore, float restoreCooldown)
        {
            val = max;
            this.max = val;
            timer = new TimerModule(restoreCooldown);
            add = addPerRestore;
        }

        public void Increase(int add)
        {
            val += add;
            val = Mathf.Clamp(val, 0, max);
        }

        public bool IsEnough(int cost)
        {
            return cost <= val;
        }
    }
}