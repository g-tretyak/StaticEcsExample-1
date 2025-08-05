using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoCameraClampToLevelBorders : IUpdateSystem
    {
        public void Update()
        {
            var level = Cxt.R.Core.Level;
            if (level == null) return;

            var player = Cxt.R.Core.MainHero;
            if (player.TryUnpack(out W.Entity e) == false) return;
            var newPos = e.Ref<RootTransformC>().val.position;

            FollowWithBounds(level.LevelBounds, newPos);
        }

        public static void FollowWithBounds(Bounds b, Vector3 playerPos)
        {
            var cam = Cxt.R.Bootstrap.MainCamera;

            var cameraPosition = cam.transform.position;

            var cameraHalfWidth = cam.orthographicSize * cam.aspect;
            var cameraHalfHeight = cam.orthographicSize;

            var minX = b.min.x + cameraHalfWidth;
            var maxX = b.max.x - cameraHalfWidth;
            var minY = b.min.y + cameraHalfHeight;
            var maxY = b.max.y - cameraHalfHeight;

            var clampedX = Mathf.Clamp(playerPos.x, minX, maxX);
            var clampedY = Mathf.Clamp(playerPos.y, minY, maxY);

            cameraPosition.x = clampedX;
            cameraPosition.y = clampedY;
            cam.transform.position = cameraPosition;
        }
    }
}