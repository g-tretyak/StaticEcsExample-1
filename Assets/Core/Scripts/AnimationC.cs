using System;
using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace ZeroZeroGames.Ecs.Modules.Unsorted.Components
{
    [Serializable]
    public struct AnimationC : IComponent
    {
        public float curDur;
        public float maxDur;
        public Action initAction;
        public Action<float, float> tickAction;
        public Action endAction;

        public AnimationC(float maxDur, Action initAction = null,
            Action<float, float> tickAction = null, Action endAction = null)
        {
            this.maxDur = maxDur;
            curDur = maxDur;

            this.initAction = initAction;
            this.tickAction = tickAction;
            this.endAction = endAction;

            this.initAction?.Invoke();
        }

        public bool Tick()
        {
            curDur -= Time.deltaTime;
            tickAction?.Invoke(curDur, maxDur);
            if (curDur <= 0)
            {
                endAction?.Invoke();
                return true;
            }

            return false;
        }

        public static void DoPingPongAnimation(float cur, float max, Action a1, Action a2)
        {
            if (cur >= max / 2f)
                a1?.Invoke();
            else
                a2?.Invoke();
        }
    }
}