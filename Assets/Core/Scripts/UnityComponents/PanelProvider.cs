using TMPro;
using UnityEngine;

namespace ZeroZeroGames.Ecs.Modules.Shared.Providers
{
    public class PanelProvider : MonoBehaviour
    {
        [SerializeField] private ImageProvider image;
        [SerializeField] private TextMeshProUGUI text;
        public RectTransform PanelRect => transform as RectTransform;

        public ImageProvider Image => image;

        public void SetText(string val)
        {
            text.text = val;
        }

        public void SetImageFill(float amount)
        {
            image.SetFill(amount);
        }

        public string GetText()
        {
            return text.text;
        }
    }
}