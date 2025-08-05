using FFS.Libraries.StaticEcs;

namespace Core.Scripts
{
    public struct ComboC : IComponent
    {
        public TimerModule timer;
        public int curAttacks;
        public int maxAttacks;
        public int damageAddOnCombo;

        public ComboC(float durVal, int maxAttacksVal, int damageAddOnComboVal)
        {
            timer = new TimerModule(durVal);
            curAttacks = 0;
            maxAttacks = maxAttacksVal;
            damageAddOnCombo = damageAddOnComboVal;
        }
    }
}