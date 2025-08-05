using UnityEngine.Events;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;

namespace ZeroZeroGames.Ecs.Modules.Shared.Helpers
{
    public static class UIHelpers
    {
        public static void AddActionOnButtonClicked(UnityAction unityAction, ButtonProvider button)
        {
            button.AddButtonAction(unityAction);
        }
    }
}