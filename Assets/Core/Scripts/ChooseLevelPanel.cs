using Client.Scripts;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.Other.Interfaces;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

public class ChooseLevelPanel : MonoBehaviour, ICanvasProvider
{
    [SerializeField] private ButtonProvider levelOne;
    [SerializeField] private ButtonProvider backToHub;

    [SerializeField] private CanvasProvider holder;

    public GameObject GetObj()
    {
        return gameObject;
    }

    public CanvasProvider Holder => holder;
    public int SortingOrder => Cxt.S.chooseLevelCanvasSortingOrder;

    public void Init()
    {
        levelOne.AddButtonAction(FulfillStartGame.StartLevel);
        backToHub.AddButtonAction(PrefabHelpers.DestroyCanvas<ChooseLevelPanel>);
    }

    public bool CanDestroy => true;
}