using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public interface IEnemyAI
    {
        public void Tick(W.Entity e);
    }
}