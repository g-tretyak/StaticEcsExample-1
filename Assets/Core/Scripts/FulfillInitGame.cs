using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Client.Scripts
{
    public struct FulfillInitGame : IInitSystem, IUpdateSystem, IDestroySystem
    {
        private EventReceiver<WT, InitGameRequested> _eventReceiver;

        public void Init()
        {
            StaticEcsHelpers.RegisterEventReceiver(out _eventReceiver);
        }

        public void Update()
        {
            foreach (var ev in _eventReceiver)
            {
                SpawnMainCamera();
                CreateAudio();
                CreateSfx();
                SpawnRootCanvas();
                SpawnSharedCanvas();
                SpawnFadeCanvas();
                SpawnMenu();
            }
        }

        private void SpawnFadeCanvas()
        {
            PrefabHelpers.CheckCreateCanvas<FadeController>();
        }

        private void CreateAudio()
        {
            var copy = PrefabHelpers.InstantiateFromPrefab(Cxt.S.audio);
            Cxt.R.Bootstrap.Audio = copy;
        }

        private void CreateSfx()
        {
            var copy = PrefabHelpers.InstantiateFromPrefab(Cxt.S.audio);
            Cxt.R.Bootstrap.Sfx = copy;
        }

        private void SpawnRootCanvas()
        {
            var copy = PrefabHelpers.InstantiateFromPrefab(Cxt.S.rootCanvas);
            Cxt.R.Bootstrap.RootCanvas = copy.Canvas;
            copy.Canvas.worldCamera = Cxt.R.Bootstrap.MainCamera;
        }

        private void SpawnMainCamera()
        {
            var copy = PrefabHelpers.InstantiateFromPrefab(Cxt.S.mainCamera);
            Cxt.R.Bootstrap.MainCamera = copy;
        }

        private void SpawnSharedCanvas()
        {
            PrefabHelpers.CheckCreateCanvas<SharedCanvasProvider>();
        }

        private void SpawnMenu()
        {
            SceneHelpers.ChangeSceneToMeta();
        }
        
        public void Destroy()
        {
            StaticEcsHelpers.DeleteEventReceiver(ref _eventReceiver);
        }
    }
}