using UnityEngine;

namespace Core.Scripts
{
    public enum InteractableType
    {
        None,
        CoffeeCup,
        PuzzlePiece,
        WinDoor
    }

    public enum PuzzlePieceType
    {
        None,
        Easy,
        Medium,
        Hard
    }

    public class InteractableProvider : MonoBehaviour
    {
        public InteractableType t;
        public PuzzlePieceType pT;
    }
}