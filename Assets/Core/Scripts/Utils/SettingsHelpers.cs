using UnityEngine;
using UnityEngine.SceneManagement;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace ZeroZeroGames.Ecs.Modules.Shared.Helpers
{
    public static class SettingsHelpers
    {
        public static void ReloadApplication()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void ChangeAudioMute()
        {
            AudioListener.pause = !AudioListener.pause;
        }

        public static void ChangeAudioVolume(float volume)
        {
            AudioListener.volume = volume;
        }

        public static void ChangeMusicVolume(float volume)
        {
            Cxt.R.Bootstrap.Audio.audioSource.volume = volume;
        }

        public static void ChangeMusicMute(bool mute)
        {
            Cxt.R.Bootstrap.Audio.audioSource.mute = mute;
        }

        public static void ToggleMusicMute()
        {
            ChangeMusicMute(!Cxt.R.Bootstrap.Audio.audioSource.mute);
        }

        public static void ChangeSfxVolume(float volume)
        {
            Cxt.R.Bootstrap.Sfx.audioSource.volume = volume;
        }

        public static void ChangeSfxMute(bool mute)
        {
            Cxt.R.Bootstrap.Sfx.audioSource.mute = mute;
        }

        public static void ToggleSfxMute()
        {
            ChangeMusicMute(!Cxt.R.Bootstrap.Sfx.audioSource.mute);
        }
    }
}