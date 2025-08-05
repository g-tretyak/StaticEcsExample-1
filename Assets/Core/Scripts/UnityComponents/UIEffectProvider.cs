using Coffee.UIEffects;
using UnityEngine;

namespace ZeroZeroGames.Ecs.Modules.Unsorted.Providers
{
    public class UIEffectProvider : MonoBehaviour
    {
        [SerializeField] private UIEffect effect;
        public UIEffect Effect => effect;
    }
}