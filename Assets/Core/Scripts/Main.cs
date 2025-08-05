using Client.Scripts;
using Core.Scripts;
using Core.Scripts.MasksTest;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;
using UnityEngine;
using ZeroZeroGames.Core.Scripts.Ecs.Modules.Shared.Unsorted.Systems;
using ZeroZeroGames.Ecs.Modules.Move.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Data;
using ZeroZeroGames.Ecs.Modules.Shared.Events;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.Tags;
using ZeroZeroGames.Ecs.Modules.Shared.World;
using ZeroZeroGames.Ecs.Modules.StartMeta.Systems;
using ZeroZeroGames.Ecs.Modules.Unsorted.Components;
using ZeroZeroGames.Ecs.Modules.Unsorted.Systems;
using ZeroZeroGames.Ecs.Modules.Unsorted.Tags;

public class Main : MonoBehaviour
{
    public StaticData staticData;

    private bool IsPaused => Time.timeScale <= 0f;

    private void Start()
    {
        W.Create(WorldConfig.Default());

        RegisterAll();
#if UNITY_EDITOR
        EcsDebug<WT>.AddWorld();
#endif
        W.Initialize();

        SetContexts();

        PrefabHelpers.InitCanvases();

        AddUnscaledSystems();
        AddBaseSystems();

        AddLateUpdateSystems();

        W.Events.Send<InitGameRequested>();
    }

    private void Update()
    {
        if (IsPaused == false)
            Sys.B.Update();

        Sys.U.Update();
    }

    private void LateUpdate()
    {
        if (IsPaused == false)
            Sys.L.Update();
    }

    private void OnDestroy()
    {
        Sys.U.Destroy();
        Sys.B.Destroy();
        Sys.L.Destroy();

        W.Destroy();
    }

    private void AddUnscaledSystems()
    {
        Sys.U.Create();
        Sys.U.AddUpdate(new DoPausePanel());
        Sys.U.AddUpdate(new FulfillAddToRootCanvas());
        Sys.U.Initialize();
#if UNITY_EDITOR
        EcsDebug<WT>.AddSystem<UnscaledSystemsType>();
#endif
    }

    private void AddBaseSystems()
    {
        Sys.B.Create();
        AddAll();
        Sys.B.Initialize();
#if UNITY_EDITOR
        EcsDebug<WT>.AddSystem<BaseSystemsType>();
#endif
    }

    private void AddLateUpdateSystems()
    {
        Sys.L.Create();

        Sys.L.AddUpdate(
            new DoCameraFollowPlayer(), 
            new DoCameraClampToLevelBorders());

        Sys.L.Initialize();

#if UNITY_EDITOR
        EcsDebug<WT>.AddSystem<LateSystemsType>();
#endif
    }

    private void RegisterAll()
    {
        W.RegisterComponentType<SceneUsedInC>();

        W.RegisterTagType<IsForDeletion>();

        W.Events.RegisterEventType<AddToRootCanvasRequested>();
        W.Events.RegisterEventType<LogRequested>();
        W.Events.RegisterEventType<InitGameRequested>();
        W.Events.RegisterEventType<MainMenuRequested>();
        W.Events.RegisterEventType<StartGameRequested>();

        W.RegisterComponentType<AnimationC>();

        W.RegisterComponentType<RootTransformC>();
        W.RegisterComponentType<MainColliderC2D>();
        W.RegisterComponentType<MoveInputC>();
        W.RegisterComponentType<MoveSpeedC>();
        W.RegisterComponentType<RootRigidbody2DC>();
        W.Events.RegisterEventType<MovementRequested>();
        W.RegisterTagType<Player>();
        W.RegisterComponentType<JumpPowerC>();
        W.RegisterComponentType<RecoilPowerC>();
        W.Events.RegisterEventType<RecoilRequested>();
        W.RegisterComponentType<HealthC>();
        W.RegisterComponentType<Triggerable>();
        W.RegisterComponentType<ShortMeleeAttackC>();
        W.Events.RegisterEventType<AttackRequested>();

        W.RegisterComponentType<KnockbackPowerC>();
        W.RegisterComponentType<ReceiveDamageBlockC>();
        W.RegisterComponentType<DamageC>();
        W.RegisterComponentType<ComboC>();
        W.RegisterComponentType<TeamC>();

        W.RegisterTagType<Enemy>();

        W.RegisterTagType<HitSuccess>();

        W.RegisterComponentType<ChargeMeleeAttackC>();

        W.RegisterComponentType<EnemyAIC>();

        W.RegisterComponentType<WanderC>();

        W.RegisterComponentType<VisualsC>();
        W.RegisterComponentType<FlipC>();

        W.RegisterComponentType<AnimatorC>();

        W.RegisterComponentType<FallC>();

        W.RegisterComponentType<DashPowerC>();

        W.RegisterMaskType<Attacking>();
        W.RegisterMaskType<Dashing>();
        W.RegisterMaskType<Jumping>();
        W.RegisterMaskType<JumpingHorizontally>();
        W.RegisterMaskType<Recoiling>();
        W.RegisterMaskType<Stunned>();

        W.RegisterComponentType<DashingChargesC>();

        W.Events.RegisterEventType<DamageDealt>();
    }

    private void SetContexts()
    {
        W.Context<StaticData>.Set(staticData);
        W.Context<RuntimeData>.Set(new RuntimeData());
    }

    private void AddAll()
    {
        Sys.B.AddUpdate(
            new FulfillLogToConsole(),
            new DoDeleteIfTagged(),
            new FulfillMainMenu(),
            new FulfillInitGame(),
            new FulfillStartGame(),
            new DoTickAnimations(),
            new DoHoverAreas(),
            new DoPlayerMoveInput(),
            new DoMovement(),
            new DoJump(),
            new DoRecoil<RecoilPowerC, Recoiling>(false, true),
            new DoRecoil<DashPowerC, Dashing>(true, false),
            new DoInteract(),
            new DoUpdateGameLevelUI(),
            new DoFlip(),
            new DoMeleeAttack());

        Sys.B.AddUpdate(
            new DoTestEnemy(), 
            new DoEnemyAi(), 
            new DoFall(), 
            new DoRestoreCharges(),
            new DoAnimate(),
            new DoDamageDealtUi());
    }
}