using Client.Scripts;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;
using ZeroZeroGames.Ecs.Modules.Unsorted.Components;

namespace Core.Scripts
{
    public struct DoPausePanel : IUpdateSystem
    {
        public void Update()
        {
            if (Cxt.R.Bootstrap.CurrentScene != SceneUsedInC.SceneUsedInType.Core) return;
            if (Input.GetKeyDown(KeyCode.Escape) == false && Input.GetKeyDown(KeyCode.Tab) == false) return;

            if (PrefabHelpers.TryGetCanvas<PausePanelProvider>())
            {
                Continue();
                return;
            }

            PrefabHelpers.CheckCreateCanvas<PausePanelProvider>();
        }

        public static void Continue()
        {
            Time.timeScale = 1f;
            FulfillStartGame.ChangeAudiosUiStatus(false);
            PrefabHelpers.DestroyCanvas<PausePanelProvider>();
        }
    }
}