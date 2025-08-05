using Client.Scripts;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.Other.Interfaces;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public class PausePanelProvider : MonoBehaviour, ICanvasProvider
    {
        [SerializeField] private CanvasProvider holder;
        [SerializeField] private ButtonProvider continueButton;
        [SerializeField] private ButtonProvider backToHubButton;
        [SerializeField] private ButtonProvider backToMenuButton;

        public GameObject GetObj()
        {
            return gameObject;
        }

        public CanvasProvider Holder => holder;
        public int SortingOrder => Cxt.S.pausePanelSortingOrder;

        public void Init()
        {
            DoPause();

            continueButton.AddButtonAction(DoPausePanel.Continue);
            backToHubButton.AddButtonAction(() =>
            {
                FulfillStartGame.DropPuzzlePiece(Cxt.R.Core.CurPuzzlePiece);
                DoMeleeAttack.IncreaseAllLootBy(
                    Cxt.S.lootDropOnPausePanelRetreatPercent / 100f * -1f);
                GoBackToHub();
            });
            
            backToHubButton.ChangeButtonInteractable(Cxt.R.Core.Hub == null);
            backToMenuButton.AddButtonAction(GoBackToMenu);
        }

        public bool CanDestroy => true;

        public static void DoPause()
        {
            Time.timeScale = 0f;
            FulfillStartGame.ChangeAudiosUiStatus(true);
        }

        public static void GoBackToHub()
        {
            DoPausePanel.Continue();

            Cxt.R.Core.RedLoot += Cxt.R.Core.RedLootCur;
            Cxt.R.Core.GreenLoot += Cxt.R.Core.GreenLootCur;
            Cxt.R.Core.BlueLoot += Cxt.R.Core.BlueLootCur;
            DoMeleeAttack.IncreaseAllLootBy(-1f);

            SceneHelpers.ChangeSceneToCore(Cxt.R.Core.GameType);
        }

        public static void GoBackToMenu()
        {
            DoPausePanel.Continue();
            SceneHelpers.ChangeSceneToMeta();
        }
    }
}