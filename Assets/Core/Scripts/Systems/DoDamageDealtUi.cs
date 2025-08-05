using FFS.Libraries.StaticEcs;
using TMPro;
using UnityEngine;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoDamageDealtUi : IInitSystem, IUpdateSystem, IDestroySystem
    {
        private EventReceiver<WT, DamageDealt> _eventReceiver;

        public void Init()
        {
            StaticEcsHelpers.RegisterEventReceiver(out _eventReceiver);
        }

        public void Update()
        {
            foreach (var ev in _eventReceiver)
            {
                var amount = ev.Value.amount;
                EventHelpers.SpawnVfx(Cxt.S.damageDealtVfxPrefab, ev.Value.pos, Cxt.S.damageDealtVfxDuration,
                    e => e.Ref<RootTransformC>().val
                        .GetComponent<TextMeshPro>().text = amount.ToString(),
                    (cur, max, e) =>
                        e.Ref<RootTransformC>().val.position +=
                            Vector3.up * (Cxt.S.damageDealtVfxSpeed * Time.deltaTime));
            }
        }

        public void Destroy()
        {
            StaticEcsHelpers.DeleteEventReceiver(ref _eventReceiver);
        }
    }
}