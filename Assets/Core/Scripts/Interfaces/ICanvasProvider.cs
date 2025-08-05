using Client.Scripts;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;

namespace ZeroZeroGames.Ecs.Modules.Shared.Other.Interfaces
{
    public interface ICanvasProvider : IGetMonoB
    {
        public CanvasProvider Holder { get; }

        public int SortingOrder { get; }

        public bool CanDestroy { get; }

        public void Init();
    }
}