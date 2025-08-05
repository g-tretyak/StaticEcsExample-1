using UnityEngine;

namespace Core.Scripts
{
    public class GameLevel : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer levelBounds;
        //[SerializeField] private InteractableProvider[] interactables;

        public Bounds LevelBounds => levelBounds.bounds;
        public InteractableProvider[] Interactables => GetComponentsInChildren<InteractableProvider>();
    }
}