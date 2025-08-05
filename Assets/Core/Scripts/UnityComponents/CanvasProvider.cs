using UnityEngine;

namespace ZeroZeroGames.Ecs.Modules.Shared.Providers
{
    public class CanvasProvider : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRect;
        [SerializeField] private Canvas canvas;

        public Canvas Canvas => canvas;
        public RectTransform CanvasRect => canvasRect;
    }
}