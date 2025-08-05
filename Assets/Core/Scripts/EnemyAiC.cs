using FFS.Libraries.StaticEcs;

namespace Core.Scripts
{
    public struct EnemyAIC : IComponent
    {
        public IEnemyAI ai;

        public EnemyAIC(IEnemyAI ai)
        {
            this.ai = ai;
        }
    }
}