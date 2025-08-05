using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoInteract : IUpdateSystem
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) == false) return;
            if (Cxt.R.Core.MainHero.TryUnpack(out W.Entity e) == false) return;
            var heroPos = e.Ref<RootTransformC>().val.position;

            var closest = float.MaxValue;
            Triggerable? closestTrigger = null;

            W.QueryComponents
                .For((W.Entity _, ref RootTransformC rootTr, ref Triggerable trigg) =>
                {
                    if (rootTr.val == null) return;
                    var entPos = rootTr.val.position;
                    if (GridHelpers.IsCloseEnoughTo(heroPos, entPos,
                            Cxt.S.mainHeroInteractRadius) == false) return;
                    if (rootTr.val.gameObject.activeInHierarchy == false) return;

                    var newDist = (heroPos - entPos).sqrMagnitude;
                    if (newDist >= closest) return;

                    closest = newDist;
                    closestTrigger = trigg;
                });

            closestTrigger?.actionOnInteract?.Invoke();
        }
    }
}