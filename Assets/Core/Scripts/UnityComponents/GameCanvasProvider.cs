using Core.Scripts;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Other.Interfaces;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;
using ZeroZeroGames.Ecs.Modules.Shared.World;
using CanvasProvider = ZeroZeroGames.Ecs.Modules.Shared.Providers.CanvasProvider;

public class GameCanvasProvider : MonoBehaviour, ICanvasProvider
{
    [SerializeField] private ImageProvider[] hearts;
    [SerializeField] private ImageProvider[] dashingCharges;
    [SerializeField] private CanvasProvider holder;
    [SerializeField] private RGBLootPanel lootPanel;
    [SerializeField] private ImageProvider invSlot;

    public GameObject GetObj()
    {
        return this.gameObject;
    }

    public CanvasProvider Holder => holder;
    public int SortingOrder => Cxt.S.gameCanvasSortingOrder;

    public void Init()
    {
    }

    public bool CanDestroy => true;

    public void DoUpdateHealth(int val)
    {
        DoUpdateSlots(hearts, val);
    }

    private void DoUpdateSlots(ImageProvider[] slots, int active)
    {
        foreach (var slot in slots)
            slot.gameObject.SetActive(false);

        for (var i = 0; i < active && i < slots.Length; i++) slots[i].gameObject.SetActive(true);
    }

    public void UpdateLoot(int r, int g, int b)
    {
        lootPanel.ChangeRGBLoot(r, g, b);
    }

    public void UpdateInventory(InteractableProvider item)
    {
        if (item == null)
        {
            invSlot.SetSprite(null);
            invSlot.gameObject.SetActive(false);
            return;
        }

        invSlot.gameObject.SetActive(true);
        invSlot.SetSprite(item.GetComponent<SpriteRenderer>().sprite);
        invSlot.SetColor(item.GetComponent<SpriteRenderer>().color);
    }

    public void DoUpdateDashingCharges(int val)
    {
        DoUpdateSlots(dashingCharges, val);
    }
}