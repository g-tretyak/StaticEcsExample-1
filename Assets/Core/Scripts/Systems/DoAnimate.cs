using System;
using Core.Scripts.MasksTest;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoAnimate : IUpdateSystem
    {
        private static readonly int State = Animator.StringToHash("state");

        private static readonly (Type, StateType)[] Checks =
        {
            (typeof(Stunned), StateType.Stunned),
            (typeof(Attacking), StateType.Attacking),
            (typeof(Dashing), StateType.Dashing),
            (typeof(Recoiling), StateType.Recoiling),
            (typeof(Jumping), StateType.Jumping)
        };

        private enum StateType
        {
            Idle,
            Stunned,
            Attacking,
            Jumping,
            Dashing,
            Recoiling
        }

        public void Update()
        {
            W.QueryComponents.For((W.Entity e, ref AnimatorC anim) =>
            {
                var res = StateType.Idle;

                for (var i = 0; i < Checks.Length; i++)
                {
                    if (e.HasAllOfMasksByType(Checks[i].Item1) == false) continue;
                    res = Checks[i].Item2;
                    break;
                }

                anim.animator.SetInteger(State, (int)res);
            });
        }
    }
}