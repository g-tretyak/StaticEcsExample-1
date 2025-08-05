using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Other.Interfaces;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

public class HubCanvasProvider : MonoBehaviour, ICanvasProvider
{
    [SerializeField] private CanvasProvider holder;
    [SerializeField] private RGBLootPanel lootPanel;

    public GameObject GetObj()
    {
        return gameObject;
    }

    public CanvasProvider Holder => holder;
    public int SortingOrder => Cxt.S.hubCanvasSortingOrder;

    public void Init()
    {
        lootPanel.ChangeRGBLoot(Cxt.R.Core.RedLoot, Cxt.R.Core.GreenLoot, Cxt.R.Core.BlueLoot);
    }

    public bool CanDestroy => true;
}