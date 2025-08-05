using System;
using Core.Scripts;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Move.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Data;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;
using ZeroZeroGames.Ecs.Modules.Shared.Tags;
using ZeroZeroGames.Ecs.Modules.Shared.World;
using ZeroZeroGames.Ecs.Modules.Unsorted.Tags;

namespace Client.Scripts
{
    public struct FulfillStartGame : IInitSystem, IUpdateSystem, IDestroySystem
    {
        private EventReceiver<WT, StartGameRequested> _eventReceiver;

        public void Init()
        {
            StaticEcsHelpers.RegisterEventReceiver(out _eventReceiver);
        }

        public void Update()
        {
            foreach (var ev in _eventReceiver)
            {
                Cxt.R.Core.GameType = ev.Value.gameType;
                switch (ev.Value.gameType)
                {
                    case RuntimeData.GameType.Main:
                        StartAsMain();
                        continue;
                    default:
                        EventHelpers.RequestLog("Не реализован - предупреждение");
                        continue;
                }
            }
        }

        public static void StartAsMain()
        {
            PrefabHelpers.CheckCreateCanvas<SharedCanvasProvider>();
            Cxt.R.Core.Hub = PrefabHelpers.InstantiateFromPrefab(Cxt.S.hub);
            PrefabHelpers.CheckCreateCanvas<HubCanvasProvider>();
            Cxt.R.Core.HoverAreas.Add((Cxt.R.Core.Hub.Quest, null, null,
                () => { PrefabHelpers.CheckCreateCanvas<ChooseLevelPanel>(); }));

            ChangeAudiosUiStatus(false);
        }

        public static void StartLevel()
        {
            PrefabHelpers.DestroyInstantiatedFromPrefab(Cxt.R.Core.Hub.gameObject);
            PrefabHelpers.DestroyCanvas<HubCanvasProvider>();
            Cxt.R.Core.HoverAreas.Clear();
            SpawnLevelWithContents();
            PrefabHelpers.DestroyCanvas<ChooseLevelPanel>();

            CreateHero();

            ChangeAudiosUiStatus(false);
        }

        public static void SpawnLevelWithContents()
        {
            if (Cxt.R.Core.Level != null)
            {
                PrefabHelpers.DestroyInstantiatedFromPrefab(Cxt.R.Core.Level.gameObject);
                W.QueryComponents
                    .For((W.Entity e, ref TeamC team) =>
                    {
                        if (team.team == TeamC.TeamType.Player) return;
                        e.SetTag<IsForDeletion>();
                    });
            }

            Cxt.R.Core.Level = PrefabHelpers.InstantiateFromPrefab(Cxt.S.levelOne);
            foreach (var el in Cxt.R.Core.Level.Interactables)
            {
                var e = EntityHelpers.CreateEntity(new RootTransformC(el.transform));
                Action act = null;
                switch (el.t)
                {
                    case InteractableType.CoffeeCup:
                        act = () =>
                        {
                            AddToPlayerHealth();
                            el.OrNull()?.gameObject.SetActive(false);
                        };
                        break;

                    case InteractableType.PuzzlePiece:
                        act = () =>
                        {
                            LootPuzzlePiece(el);
                            el.OrNull()?.gameObject.SetActive(false);
                        };
                        break;

                    case InteractableType.WinDoor:
                        act = () => { PrefabHelpers.CheckCreateCanvas<WinDoorCanvasProvider>(); };
                        break;

                    case InteractableType.None:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                e.Add(new Triggerable(act));
            }
        }

        public static void DropPuzzlePiece(InteractableProvider interactable)
        {
            if (interactable == null) return;
            interactable.transform.SetParent(null, true);
            interactable.gameObject.SetActive(true);
        }

        public static void LootPuzzlePiece(InteractableProvider interactable)
        {
            var pl = Cxt.R.Core.MainHero;
            if (pl.TryUnpack(out W.Entity e) == false) return;
            var tr = e.Ref<RootTransformC>();
            var cur = Cxt.R.Core.CurPuzzlePiece;
            DropPuzzlePiece(cur);
            interactable.transform.SetParent(tr.val, true);
            interactable.transform.localPosition = Vector3.zero;
            Cxt.R.Core.CurPuzzlePiece = interactable;
        }

        public static void AddToPlayerHealth()
        {
            if (Cxt.R.Core.MainHero.TryUnpack(out W.Entity e) == false) return;
            if (e.HasAllOf<HealthC>() == false) return;
            e.Ref<HealthC>().val += Cxt.S.coffeeCupHealthAdd;
        }

        public static void CreateHero()
        {
            var obj = PrefabHelpers.InstantiateFromPrefab(Cxt.S.mainHero);
            var ent = EntityHelpers.CreateEntity(new RootTransformC(obj.transform));
            Cxt.R.Core.MainHero = ent.Gid();
            ent.Add(new RootRigidbody2DC(obj.GetComponent<Rigidbody2D>()));
            ent.Add(new MainColliderC2D(obj.GetComponent<Collider2D>()));
            ent.Add(new MoveSpeedC(Cxt.S.mainHeroSpeed));
            ent.Add(new JumpPowerC(Cxt.S.mainHeroJumpPower));
            ent.Add(new RecoilPowerC(Cxt.S.mainHeroRecoilPower, Cxt.S.mainHeroRecoilCooldown,
                Cxt.S.mainHeroRecoilDuration, Cxt.S.mainHeroRecoilCost));
            ent.Add(new DashPowerC(Cxt.S.mainHeroDashPower, Cxt.S.mainHeroDashCooldown,
                Cxt.S.mainHeroDashDuration, Cxt.S.mainHeroDashCost));
            ent.Add(new ShortMeleeAttackC(Cxt.S.mainHeroAttackPower, Cxt.S.mainHeroAttackCooldown,
                Cxt.S.mainHeroAttackDuration));
            ent.Add<MoveInputC>();
            ent.Add(new HealthC(Cxt.S.maxHealthHero));
            ent.Add(new ComboC(Cxt.S.mainHeroComboDur, Cxt.S.mainHeroComboMaxAttacks, Cxt.S.mainHeroComboDamageAdd));
            ent.Add(new DamageC(Cxt.S.heroDamage));
            ent.Add(new KnockbackPowerC(Cxt.S.knockbackPower, Cxt.S.knockbackCooldown, Cxt.S.knockbackDur));
            ent.Add(new TeamC(TeamC.TeamType.Player));
            ent.Add(new VisualsC(obj.GetComponentInChildren<SpriteRenderer>()));
            ent.Add(new AnimatorC(obj.GetComponentInChildren<Animator>()));
            ent.Add(new FlipC { lastNonZeroX = 1 });
            ent.Add(new FallC());

            ent.Add(new DashingChargesC(Cxt.S.dashingChargesMax, Cxt.S.dashingChargesAdd,
                Cxt.S.dashingChargesCooldown));
            ent.SetTag<Player>();
        }

        public static void ChangeAudiosUiStatus(bool status)
        {
            var shared = PrefabHelpers.CheckCreateCanvas<SharedCanvasProvider>();
            shared.ChangeAudiosUiStatus(status);
        }

        public void Destroy()
        {
            StaticEcsHelpers.DeleteEventReceiver(ref _eventReceiver);
        }
    }
}