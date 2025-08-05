using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZeroZeroGames.Ecs.Modules.Shared.Providers
{
    public class SliderProvider : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void AddSliderAction(UnityAction<float> action)
        {
            slider.onValueChanged.AddListener(action);
        }

        public void SetWithoutNotify(float value)
        {
            slider.SetValueWithoutNotify(value);
        }
    }
}