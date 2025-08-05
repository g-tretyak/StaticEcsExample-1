namespace Core.Scripts
{
    public interface IDash
    {
        public float Value { get; }
        public TimerModule UsingMod { get; }
        public TimerModule CooldownMod { get; }

        public int Cost { get; }
    }
}