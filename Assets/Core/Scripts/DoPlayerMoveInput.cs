using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.Tags;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace ZeroZeroGames.Ecs.Modules.Unsorted.Systems
{
    public struct DoPlayerMoveInput : IUpdateSystem
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        private Vector2Int GetAxisRaw()
        {
            var xInput = Input.GetAxisRaw(HorizontalAxis);
            var yInput = Input.GetAxisRaw(VerticalAxis);

            return GridHelpers.ConvertDirectionToInt(new Vector2(xInput, yInput));
        }

        public void Update()
        {
            var axis = GetAxisRaw();

            W.QueryComponents
                .With<TagAll<Player>>()
                .For((W.Entity entity, ref MoveInputC moveDirection) =>
                {
                    moveDirection.SetVal(axis);
                });
        }
    }
}