using System;
using Client.Scripts;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Events;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;
using ZeroZeroGames.Ecs.Modules.Shared.World;
using ZeroZeroGames.Ecs.Modules.Unsorted.Components;
using ZeroZeroGames.Ecs.Modules.Unsorted.Tags;

namespace ZeroZeroGames.Ecs.Modules.Shared.Helpers
{
    public static class EventHelpers
    {
        public static void RequestLog(string msg)
        {
            W.Events.Send(new LogRequested { message = msg });
        }

        public static void RequestAddToRootCanvas(CanvasProvider childCanvas, int sortingOrder)
        {
            W.Events.Send(new AddToRootCanvasRequested(childCanvas, sortingOrder));
        }

        public static void RequestMainMenu()
        {
            W.Events.Send(new MainMenuRequested());
        }

        public static void PlayAudio(AudioClip song)
        {
            Cxt.R.Bootstrap.Audio.audioSource.clip = song;
            Cxt.R.Bootstrap.Audio.audioSource.Play();
            Cxt.R.Bootstrap.Audio.audioSource.volume = Cxt.S.musicVolume;
        }

        public static void SpawnVfx(GameObject prefab, Vector2 pos, float lifeDur,
            Action<W.Entity> onStart = null, Action<float, float, W.Entity> onTick = null)
        {
            var go = PrefabHelpers.InstantiateFromPrefab(prefab);
            go.transform.position = pos;
            var e = EntityHelpers.CreateEntity(new RootTransformC(go.transform));
            var gid = e.Gid();
            e.Add(new AnimationC(lifeDur,
                () => onStart?.Invoke(e),
                (cur, max) => onTick?.Invoke(cur, max, e),
                () =>
                {
                    if (gid.TryUnpack(out W.Entity ent) == false) return;
                    ent.SetTag<IsForDeletion>();
                }));
        }
    }
}