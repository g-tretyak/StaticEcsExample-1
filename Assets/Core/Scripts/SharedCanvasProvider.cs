using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.Other.Interfaces;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace ZeroZeroGames.Ecs.Modules.Shared.Providers
{
    public class SharedCanvasProvider : MonoBehaviour, ICanvasProvider
    {
        [SerializeField] private CanvasProvider holder;

        [SerializeField] private ButtonProvider menuButton;

        [SerializeField] private ButtonProvider muteMusicButton;
        [SerializeField] private SliderProvider musicSlider;

        [SerializeField] private ButtonProvider muteSfxButton;
        [SerializeField] private SliderProvider sfxSlider;

        [SerializeField] private PanelProvider versionPanel;
        public CanvasProvider Holder => holder;
        public int SortingOrder => Cxt.S.sharedCanvasSortingOrder;
        public bool CanDestroy => false;

        public void Init()
        {
            versionPanel.SetText(Application.version);
            UIHelpers.AddActionOnButtonClicked(SceneHelpers.ChangeSceneToMeta, menuButton);


            UIHelpers.AddActionOnButtonClicked(SettingsHelpers.ToggleMusicMute, muteMusicButton);
            musicSlider.AddSliderAction(SettingsHelpers.ChangeMusicVolume);
            SettingsHelpers.ChangeMusicVolume(Cxt.S.musicVolume);
            musicSlider.SetWithoutNotify(Cxt.S.musicVolume);
            SettingsHelpers.ChangeMusicMute(Cxt.S.musicInitialStatus);

            UIHelpers.AddActionOnButtonClicked(SettingsHelpers.ToggleSfxMute, muteSfxButton);
            sfxSlider.AddSliderAction(SettingsHelpers.ChangeSfxVolume);
            SettingsHelpers.ChangeSfxVolume(Cxt.S.sfxVolume);
            sfxSlider.SetWithoutNotify(Cxt.S.sfxVolume);
            SettingsHelpers.ChangeSfxMute(Cxt.S.sfxInitialStatus);
        }

        public GameObject GetObj()
        {
            return gameObject;
        }

        public MonoBehaviour GetMono()
        {
            return this;
        }

        public void ChangeAudiosUiStatus(bool status)
        {
            muteMusicButton.gameObject.SetActive(status);
            musicSlider.gameObject.SetActive(status);

            muteSfxButton.gameObject.SetActive(status);
            sfxSlider.gameObject.SetActive(status);
        }
    }
}