using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Data;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;

namespace Client.Scripts
{
    public class MainMenuPanel : MonoBehaviour
    {
        [SerializeField] private ButtonProvider mainGame,
            formulasTutorial,
            filtersTutorial;

        public void Init()
        {
            mainGame.AddButtonAction(() => SceneHelpers
                .ChangeSceneToCore(RuntimeData.GameType.Main));
        }
    }
}