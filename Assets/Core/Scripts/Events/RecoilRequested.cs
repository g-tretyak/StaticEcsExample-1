using FFS.Libraries.StaticEcs;

namespace Core.Scripts
{
    public struct RecoilRequested : IEvent
    {
        public EntityGID ent;

        public RecoilRequested(EntityGID ent)
        {
            this.ent = ent;
        }
    }
}