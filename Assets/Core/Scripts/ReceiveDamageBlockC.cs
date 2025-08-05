using FFS.Libraries.StaticEcs;

namespace Core.Scripts
{
    public struct ReceiveDamageBlockC : IComponent
    {
        public TimerModule timer;

        public ReceiveDamageBlockC(float dur)
        {
            timer = new TimerModule(dur);
        }
    }
}