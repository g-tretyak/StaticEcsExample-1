using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public interface IAttack
    {
        public void OnStart(W.Entity e);

        public void OnTick(W.Entity e);

        public bool CanUse();

        public void DoCleanup(W.Entity ent);
    }
}