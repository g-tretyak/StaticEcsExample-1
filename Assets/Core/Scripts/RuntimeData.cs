using System;
using System.Collections.Generic;
using Client.Scripts;
using Core.Scripts;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.Other.Interfaces;
using ZeroZeroGames.Ecs.Modules.Shared.World;
using ZeroZeroGames.Ecs.Modules.Unsorted.Components;
using ZeroZeroGames.Ecs.Modules.Unsorted.Tags;

namespace ZeroZeroGames.Ecs.Modules.Shared.Data
{
    public class RuntimeData
    {
        public enum GameType
        {
            None,
            Main
        }

        public BootstrapS Bootstrap { get; set; } = new();

        public MetaS Meta { get; set; } = new();

        public CoreS Core { get; set; } = new();

        public class BootstrapS
        {
            private MonobDictionary<ICanvasProvider> Canvases { get; } = new();

            public Camera MainCamera { get; set; }

            public Canvas RootCanvas { get; set; }
            public SceneUsedInC.SceneUsedInType CurrentScene { get; set; }
            
            public MusicProvider Audio { get; set; }

            public MusicProvider Sfx { get; set; }

            public MonobDictionary<ICanvasProvider> GetCanvases()
            {
                return Canvases;
            }

            public void ResetSaved()
            {
            }

            public void DestroyCanvases()
            {
                var objs = Canvases.GetAliveObjs();

                foreach (var obj in objs)
                {
                    if (obj.TryGetComponent(out ICanvasProvider pr) == false) continue;
                    if (pr.CanDestroy == false) continue;
                    Canvases.RemoveFromObjsWithDestroy(pr);
                }
            }
        }

        public class MetaS
        {
            public void Clear()
            {
                Cxt.R.Bootstrap.DestroyCanvases();
            }
        }

        public class ObjPrefabCopyPair<T2> where T2 : IGetMonoB
        {
            public T2 copy;
            public T2 prefab;
        }

        public class CoreS
        {
            public GameType GameType { get; set; }
            public HubLevel Hub { get; set; }
            public int RedLoot { get; set; }
            public int GreenLoot { get; set; }
            public int BlueLoot { get; set; }

            public List<(SpriteRenderer sp, Action onOutside, Action onInside, Action onClick)> HoverAreas { get; set; } = new();

            public GameLevel Level { get; set; }
            public EntityGID MainHero { get; set; }
            public int RedLootCur { get; set; }
            public int GreenLootCur { get; set; }
            public int BlueLootCur { get; set; }
            public InteractableProvider CurPuzzlePiece { get; set; }

            public void Clear()
            {
                Cxt.R.Bootstrap.DestroyCanvases();
                GameType = default;
                PrefabHelpers.DestroyInstantiatedFromPrefab(Hub.OrNull()?.gameObject);
                HoverAreas.Clear();
                PrefabHelpers.DestroyInstantiatedFromPrefab(Level.OrNull()?.gameObject);
                if (MainHero.TryUnpack(out W.Entity heroE)) heroE.SetTag<IsForDeletion>();
                var camPos = Cxt.R.Bootstrap.MainCamera.transform.position;
                camPos.x = 0;
                camPos.y = 0;
                Cxt.R.Bootstrap.MainCamera.transform.position = camPos;
                PrefabHelpers.DestroyInstantiatedFromPrefab(CurPuzzlePiece.OrNull()?.gameObject);
            }
        }
    }
}