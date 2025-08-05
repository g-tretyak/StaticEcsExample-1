using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Client.Scripts
{
    public struct FulfillMainMenu : IInitSystem, IUpdateSystem, IDestroySystem
    {
        private EventReceiver<WT, MainMenuRequested> _eventReceiver;

        public void Init()
        {
            StaticEcsHelpers.RegisterEventReceiver(out _eventReceiver);
        }

        public void Update()
        {
            foreach (var ev in _eventReceiver)
            {
                PrefabHelpers.CheckCreateCanvas<MainMenuCanvasProvider>();
                FulfillStartGame.ChangeAudiosUiStatus(true);
            }
        }

        public void Destroy()
        {
            StaticEcsHelpers.DeleteEventReceiver(ref _eventReceiver);
        }
    }
}