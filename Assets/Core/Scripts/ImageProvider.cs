using UnityEngine;
using UnityEngine.UI;
using ZeroZeroGames.Ecs.Modules.Unsorted.Providers;

namespace ZeroZeroGames.Ecs.Modules.Shared.Providers
{
    public class ImageProvider : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private UIEffectProvider effect;

        public UIEffectProvider Effect => effect;

        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            image.color = color;
        }

        public void SetAlpha(float alpha)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }

        public void SetFill(float fill)
        {
            image.fillAmount = fill;
        }
    }
}