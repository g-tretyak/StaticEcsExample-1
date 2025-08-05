using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Data;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.Other.Interfaces;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

public class MainMenuCanvasProvider : MonoBehaviour, ICanvasProvider
{
    [SerializeField] private ButtonProvider startGame;
    [SerializeField] private ButtonProvider exitGame;

    [SerializeField] private CanvasProvider holder;
    public CanvasProvider Holder => holder;
    public int SortingOrder => Cxt.S.mainMenuCanvasSortingOrder;

    public bool CanDestroy => true;

    public void Init()
    {
        startGame.AddButtonAction(() => SceneHelpers
            .ChangeSceneToCore(RuntimeData.GameType.Main));

        exitGame.gameObject.SetActive(false);

#if !UNITY_WEBGL
        exitGame.gameObject.SetActive(true);
        exitGame.AddButtonAction(Application.Quit);
#endif
    }

    public GameObject GetObj()
    {
        return gameObject;
    }
}