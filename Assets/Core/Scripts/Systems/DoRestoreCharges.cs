using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.World;

namespace Core.Scripts
{
    public struct DoRestoreCharges : IUpdateSystem
    {
        public void Update()
        {
            W.QueryComponents.For((W.Entity _, ref DashingChargesC charges) =>
            {
                charges.timer.TickTimer();
                if (charges.timer.OnTimer == false)
                {
                    charges.timer.SetTimer();
                    charges.Increase(charges.add);
                }
            });
        }
    }
}