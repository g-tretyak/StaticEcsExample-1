using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.Data;

namespace Client.Scripts
{
    public struct StartGameRequested : IEvent
    {
        public RuntimeData.GameType gameType;

        public StartGameRequested(RuntimeData.GameType gameType)
        {
            this.gameType = gameType;
        }
    }
}