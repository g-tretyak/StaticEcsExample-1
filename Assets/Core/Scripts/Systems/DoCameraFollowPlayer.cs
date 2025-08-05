using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.Components;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoCameraFollowPlayer : IUpdateSystem
    {
        public void Update()
        {
            var pos = Cxt.R.Bootstrap.MainCamera.transform.position;
            var player = Cxt.R.Core.MainHero;
            if (player.TryUnpack(out W.Entity e) == false)
            {
                pos.x = 0;
                pos.y = 0;
            }
            else
            {
                var newPos = e.Ref<RootTransformC>().val.position;
                pos.x = newPos.x;
                pos.y = newPos.y;
            }

            Cxt.R.Bootstrap.MainCamera.transform.position = pos;
        }
    }
}