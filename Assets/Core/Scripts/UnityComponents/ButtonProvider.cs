using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZeroZeroGames.Ecs.Modules.Shared.Providers
{
    public class ButtonProvider : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;
        public RectTransform PanelRect => transform as RectTransform;

        public void AddButtonAction(UnityAction action)
        {
            button.onClick.AddListener(action);
        }

        public void ClearButtonActions()
        {
            button.onClick.RemoveAllListeners();
        }

        public void ChangeButtonInteractable(bool interactable)
        {
            button.interactable = interactable;
        }

        public void SetText(string s)
        {
            text.text = s;
        }
    }
}