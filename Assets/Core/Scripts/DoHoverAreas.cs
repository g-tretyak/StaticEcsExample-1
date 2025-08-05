using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Client.Scripts
{
    public struct DoHoverAreas : IUpdateSystem
    {
        public void Update()
        {
            foreach (var el in Cxt.R.Core.HoverAreas)
            {
                if (el.sp.gameObject.activeInHierarchy == false) continue;

                var worldMouseP = Cxt.R.Bootstrap.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                worldMouseP.z = 0;

                if (el.sp.bounds.Contains(worldMouseP) == false)
                {
                    el.onOutside?.Invoke();
                    continue;
                }

                el.onInside?.Invoke();

                if (Input.GetKeyDown(KeyCode.Mouse0)) el.onClick?.Invoke();
            }
        }
    }
}