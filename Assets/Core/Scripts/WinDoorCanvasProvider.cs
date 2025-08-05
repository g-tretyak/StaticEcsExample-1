using Client.Scripts;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.Other.Interfaces;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public class WinDoorCanvasProvider : MonoBehaviour, ICanvasProvider
    {
        [SerializeField] private CanvasProvider holder;
        [SerializeField] private ButtonProvider goBack;
        [SerializeField] private ButtonProvider goNext;

        public GameObject GetObj()
        {
            return gameObject;
        }

        public CanvasProvider Holder => holder;
        public int SortingOrder => Cxt.S.winDoorSortingOrder;

        public void Init()
        {
            PausePanelProvider.DoPause();

            goBack.AddButtonAction(() =>
            {
                FulfillStartGame.DropPuzzlePiece(Cxt.R.Core.CurPuzzlePiece);

                PausePanelProvider.GoBackToHub();
                PrefabHelpers.DestroyCanvas<WinDoorCanvasProvider>();
            });

            goNext.AddButtonAction(() =>
            {
                DoPausePanel.Continue();
                FulfillStartGame.SpawnLevelWithContents();
                if (Cxt.R.Core.MainHero.TryUnpack(out W.Entity e))
                    e.Ref<RootTransformC>().val.position = Vector3.zero;
                PrefabHelpers.DestroyCanvas<WinDoorCanvasProvider>();
            });
        }

        public bool CanDestroy => true;
    }
}