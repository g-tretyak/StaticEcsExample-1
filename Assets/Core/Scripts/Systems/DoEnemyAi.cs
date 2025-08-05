using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoEnemyAi : IUpdateSystem
    {
        public void Update()
        {
            W.QueryComponents
                .For((W.Entity e, ref EnemyAIC ai) => { ai.ai.Tick(e); });
        }
    }
}