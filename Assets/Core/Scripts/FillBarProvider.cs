using UnityEngine;

namespace ZeroZeroGames.Ecs.Modules.Shared.Providers.UI
{
    public class FillBarProvider : MonoBehaviour
    {
        [SerializeField] private PanelProvider fill;
        [SerializeField] private ImageProvider bg;

        public PanelProvider Fill => fill;
        public ImageProvider Bg => bg;

        public void Tick(float curDur, float maxDur, string fillText, Color fillColor, bool initiallyEmpty)
        {
            fill.SetText(fillText);

            fill.Image.SetColor(fillColor);

            var finalFill = curDur / maxDur;

            if (initiallyEmpty)
                finalFill = 1f - finalFill;

            fill.SetImageFill(finalFill);
        }
    }
}