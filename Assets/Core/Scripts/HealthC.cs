using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;

namespace Core.Scripts
{
    public struct HealthC : IComponent
    {
        [StaticEcsEditorTableValue] public int val;

        public int max;

        public void ResetToMax()
        {
            val = max;
        }

        public HealthC(int v)
        {
            val = v;
            max = v;
        }
    }
}