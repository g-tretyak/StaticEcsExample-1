using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace Core.Scripts
{
    public struct DamageDealt : IEvent
    {
        public EntityGID receiver;
        public int amount;
        public Vector2 pos;

        public DamageDealt(EntityGID receiver, int amount, Vector2 pos)
        {
            this.receiver = receiver;
            this.amount = amount;
            this.pos = pos;
        }
    }
}