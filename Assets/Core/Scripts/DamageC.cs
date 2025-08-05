using FFS.Libraries.StaticEcs;

namespace Core.Scripts
{
    public struct DamageC : IComponent
    {
        public int val;

        public DamageC(int val)
        {
            this.val = val;
        }
    }
}