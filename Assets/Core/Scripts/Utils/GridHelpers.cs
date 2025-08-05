using UnityEngine;

namespace ZeroZeroGames.Ecs.Modules.Shared.Helpers
{
    public static class GridHelpers
    {
        public static int GetValueSignIfNotZero(float value)
        {
            if (value == 0) return 0;
            return (int)Mathf.Sign(value);
        }

        public static Vector2Int ConvertDirectionToInt(Vector2 dir)
        {
            return new Vector2Int(GetValueSignIfNotZero(dir.x), GetValueSignIfNotZero(dir.y));
        }

        public static Vector2Int[] GetFourDirs()
        {
            return new[]
            {
                Vector2Int.left,
                Vector2Int.right,
                Vector2Int.up,
                Vector2Int.down
            };
        }

        public static Vector2Int RemoveDiagonalDir(Vector2Int dir, Vector2Int[] allowedDirs = null)
        {
            if (dir == Vector2Int.zero)
                return dir;

            var dirs = allowedDirs ?? GetFourDirs();

            var maxDot = -1f;
            var closestDir = dirs[0];

            for (var i = 0; i < dirs.Length; i++)
            {
                var dot = Vector2.Dot(dir, dirs[i]);
                if (dot <= maxDot) continue;
                maxDot = dot;
                closestDir = dirs[i];
            }

            return closestDir;
        }

        public static Vector2 GetTargetPos(Vector2 pos, Vector2 dir)
        {
            return pos + dir;
        }

        public static bool IsCloseEnoughTo(Vector2 callerPos, Vector2 targetPos, float dist)
        {
            return (callerPos - targetPos).sqrMagnitude <= dist * dist;
        }

        public static bool IsCloseEnoughTo(float callerCoord, float targetCoord, float dist)
        {
            return Mathf.Abs(callerCoord - targetCoord) <= dist;
        }

        public static void FinalizeRb2DMove(Rigidbody2D caller, Vector2 targetPos)
        {
            caller.linearVelocity = Vector3.zero;
            caller.position = targetPos;
        }

        public static Vector3 GetDirTo(Vector2 callerPos, Vector2 targetPos)
        {
            var dir = targetPos - callerPos;
            dir.Normalize();
            return dir;
        }

        public static Vector3 GetPosWithZ(Vector3 originalPos)
        {
            var newPosition = originalPos;
            newPosition.z = originalPos.y * -1f;
            return newPosition;
        }
    }
}