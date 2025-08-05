using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;

public class RGBLootPanel : MonoBehaviour
{
    [SerializeField] private PanelProvider red, green, blue;

    public void ChangeRed(int val)
    {
        red.SetText(val.ToString());
    }

    public void ChangeGreen(int val)
    {
        green.SetText(val.ToString());
    }

    public void ChangeBlue(int val)
    {
        blue.SetText(val.ToString());
    }

    public void ChangeRGBLoot(int redVal, int greenVal, int blueVal)
    {
        ChangeRed(redVal);
        ChangeGreen(greenVal);
        ChangeBlue(blueVal);
    }
}