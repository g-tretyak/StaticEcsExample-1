using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.Helpers;
using ZeroZeroGames.Ecs.Modules.Shared.World;
using ZeroZeroGames.Ecs.Modules.Unsorted.Tags;

namespace Core.Scripts
{
    public struct DoUpdateGameLevelUI : IUpdateSystem
    {
        public void Update()
        {
            if (Cxt.R.Core.MainHero.TryUnpack(out W.Entity e) == false) return;
            if (e.HasAllOfTags<IsForDeletion>()) return;
            var canv = PrefabHelpers.CheckCreateCanvas<GameCanvasProvider>();
            canv.DoUpdateHealth(e.Ref<HealthC>().val);
            canv.DoUpdateDashingCharges(e.Ref<DashingChargesC>().val);
            canv.UpdateLoot(Cxt.R.Core.RedLootCur, Cxt.R.Core.GreenLootCur, Cxt.R.Core.BlueLootCur);

            canv.UpdateInventory(Cxt.R.Core.CurPuzzlePiece);
        }
    }
}