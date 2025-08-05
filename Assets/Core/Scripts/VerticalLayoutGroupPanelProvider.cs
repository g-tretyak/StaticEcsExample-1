using UnityEngine;
using UnityEngine.UI;

namespace ZeroZeroGames.Ecs.Modules.Shared.Providers
{
    public class VerticalLayoutGroupPanelProvider : MonoBehaviour
    {
        [SerializeField] private ImageProvider bg;
        [SerializeField] private VerticalLayoutGroup group;
        [SerializeField] private CanvasGroup panelGroup;

        public ImageProvider Bg => bg;

        public void ChangeCanvasGroupAlpha(float alpha)
        {
            panelGroup.alpha = alpha;
        }

        public void ChangeBgObjStatus(bool status)
        {
            bg.gameObject.SetActive(status);
        }

        public Transform GetGroupTransform()
        {
            return group.transform;
        }
    }
}