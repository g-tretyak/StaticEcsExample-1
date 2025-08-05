using FFS.Libraries.StaticEcs;

namespace Core.Scripts
{
    public struct TeamC : IComponent
    {
        public enum TeamType
        {
            None,
            Player,
            Enemy,
            Environment
        }

        public TeamType team;

        public TeamC(TeamType team)
        {
            this.team = team;
        }
    }
}