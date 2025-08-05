using FFS.Libraries.StaticEcs;

namespace Core.Scripts
{
    public struct AttackRequested : IEvent
    {
        public EntityGID ent;
        public IAttack attack;

        public AttackRequested(EntityGID ent, IAttack attack)
        {
            this.ent = ent;
            this.attack = attack;
        }
    }
}