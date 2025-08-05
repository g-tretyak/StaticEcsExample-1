using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Other.Interfaces;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace ZeroZeroGames.Ecs.Modules.Shared.Helpers
{
    public static class PrefabHelpers
    {
        public static T OrNull<T>(this T obj) where T : Object
        {
            return obj ? obj : null;
        }

        public static T InstantiateFromPrefab<T>(T prefab)
            where T : Object
        {
            var ob = Object.Instantiate(prefab);
            return ob;
        }

        public static void DestroyInstantiatedFromPrefab(GameObject gameObject)
        {
            if (gameObject == null) return;
            Object.Destroy(gameObject);
        }

        public static T TryGetCanvas<T>() where T : MonoBehaviour, ICanvasProvider
        {
            var canvases = Cxt.R.Bootstrap.GetCanvases();
            return canvases.HasObj<T>() == false ? null : canvases.GetObj<T>();
        }

        public static T CheckCreateCanvas<T>() where T : MonoBehaviour, ICanvasProvider
        {
            var canvases = Cxt.R.Bootstrap.GetCanvases();
            var hadObj = canvases.HasObj<T>();
            var copy = canvases.GetOrCreateObj<T>();

            if (hadObj == false)
            {
                copy.Init();
                EventHelpers.RequestAddToRootCanvas(copy.Holder, copy.SortingOrder);
            }

            return copy;
        }

        public static void DestroyCanvas<T>() where T : MonoBehaviour, ICanvasProvider
        {
            var canvases = Cxt.R.Bootstrap.GetCanvases();
            canvases.RemoveFromObjsWithDestroy<T>();
        }

        public static void InitCanvases()
        {
            var canvases = Cxt.R.Bootstrap.GetCanvases();
            canvases.RegisterCanvasObj(Cxt.S.mainMainMenuCanvas);
            canvases.RegisterCanvasObj(Cxt.S.fadeCanvas);
            canvases.RegisterCanvasObj(Cxt.S.sharedCanvas);
            canvases.RegisterCanvasObj(Cxt.S.hubCanvas);
            canvases.RegisterCanvasObj(Cxt.S.chooseLevelCanvas);
            canvases.RegisterCanvasObj(Cxt.S.pauseMenuPanel);
            canvases.RegisterCanvasObj(Cxt.S.gameLevelCanvas);
            canvases.RegisterCanvasObj(Cxt.S.winDoorCanvas);

            canvases.DebugDict();
        }
    }
}