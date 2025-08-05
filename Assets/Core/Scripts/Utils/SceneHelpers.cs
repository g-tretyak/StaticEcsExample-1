using System;
using Client.Scripts;
using ZeroZeroGames.Ecs.Modules.Shared.Data;
using ZeroZeroGames.Ecs.Modules.Shared.World;
using ZeroZeroGames.Ecs.Modules.Unsorted.Components;
using ZeroZeroGames.Ecs.Modules.Unsorted.Tags;

namespace ZeroZeroGames.Ecs.Modules.Shared.Helpers
{
    public static class SceneHelpers
    {
        private static void ClearMetaSceneContents()
        {
            IterateWithSceneCheck(SceneUsedInC.SceneUsedInType.Meta);
            Cxt.R.Meta.Clear();
        }

        private static void ClearCoreSceneContents()
        {
            IterateWithSceneCheck(SceneUsedInC.SceneUsedInType.Core);
            Cxt.R.Core.Clear();
        }

        private static void IterateWithSceneCheck(SceneUsedInC.SceneUsedInType sceneType)
        {
            W.QueryComponents.For((W.Entity e, ref SceneUsedInC scene) =>
            {
                if (scene.val != sceneType) return;
                if (scene.val == SceneUsedInC.SceneUsedInType.Bootstrap) return;
                e.SetTag<IsForDeletion>();
            });
        }

        public static void ChangeSceneToMeta()
        {
            ClearAllSceneContents();

            Cxt.R.Bootstrap.CurrentScene = SceneUsedInC.SceneUsedInType.Meta;

            EventHelpers.RequestMainMenu();
        }

        private static void ClearAllSceneContents()
        {
            ClearMetaSceneContents();
            ClearCoreSceneContents();
        }

        public static void ChangeSceneToCore(RuntimeData.GameType gameType)
        {
            ClearAllSceneContents();

            Cxt.R.Bootstrap.CurrentScene = SceneUsedInC.SceneUsedInType.Core;

            RequestStartGame(gameType);
        }

        public static void ReloadScene()
        {
            switch (Cxt.R.Bootstrap.CurrentScene)
            {
                case SceneUsedInC.SceneUsedInType.Core:
                    ChangeSceneToCore(Cxt.R.Core.GameType);
                    return;
                case SceneUsedInC.SceneUsedInType.Meta:
                    ChangeSceneToMeta();
                    return;
                case SceneUsedInC.SceneUsedInType.Bootstrap:
                default:
                    throw new NotImplementedException("Текущая сцена не может быть перезагружена!");
            }
        }

        private static void RequestStartGame(RuntimeData.GameType gameType)
        {
            W.Events.Send(new StartGameRequested(gameType));
        }
    }
}