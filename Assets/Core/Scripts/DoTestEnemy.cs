using FFS.Libraries.StaticEcs;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Move.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoTestEnemy : IUpdateSystem
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) == false) return;
            var worldMouse = Cxt.R.Bootstrap.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldMouse.z = 0;
            var ent = CreateEnemy(worldMouse);
            ent.Add(new ShortMeleeAttackC(Cxt.S.enemySlowAttackPower, Cxt.S.enemySlowAttackCooldown,
                Cxt.S.enemySlowAttackDuration));
            ent.Add(new ChargeMeleeAttackC(Cxt.S.enemyChargeAttackPower, Cxt.S.enemyChargeAttackCooldown,
                Cxt.S.enemyChargeAttackDuration));
            ent.Add(new WanderC { direction = 1f });
            ent.Add(new EnemyAIC(new BasicEnemyAi()));
        }

        public static W.Entity CreateEnemy(Vector2 pos)
        {
            var obj = PrefabHelpers.InstantiateFromPrefab(Cxt.S.enemy);
            var ent = EntityHelpers.CreateEntity(new RootTransformC(obj.transform));
            ent.Add(new RootRigidbody2DC(obj.GetComponent<Rigidbody2D>()));
            ent.Add(new MainColliderC2D(obj.GetComponent<Collider2D>()));
            ent.Add(new MoveSpeedC(Cxt.S.enemySpeed));
            ent.Add<MoveInputC>();
            ent.Add(new HealthC(Cxt.S.maxHealthEnemy));
            ent.Add(new DamageC(Cxt.S.enemyDamage));
            ent.Add(new KnockbackPowerC(Cxt.S.knockbackPower, Cxt.S.knockbackCooldown, Cxt.S.knockbackDur));
            ent.Add(new TeamC(TeamC.TeamType.Enemy));
            ent.SetTag<Enemy>();
            ent.Ref<RootRigidbody2DC>().val.position = pos;
            ent.Add(new VisualsC(obj.GetComponentInChildren<SpriteRenderer>()));
            ent.Add(new AnimatorC(obj.GetComponentInChildren<Animator>()));
            ent.Add(new FlipC { lastNonZeroX = 1 });
            ent.Add(new FallC());

            return ent;
        }
    }
}