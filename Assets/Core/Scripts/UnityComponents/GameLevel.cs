using UnityEngine;

namespace Core.Scripts
{
    public class GameLevel : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer levelBounds;

        public Bounds LevelBounds => levelBounds.bounds;
        public InteractableProvider[] Interactables => GetComponentsInChildren<InteractableProvider>();
    }
}