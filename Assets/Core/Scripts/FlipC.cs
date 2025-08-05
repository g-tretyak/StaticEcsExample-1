using FFS.Libraries.StaticEcs;

namespace Core.Scripts
{
    public struct FlipC : IComponent
    {
        public int lastNonZeroX;

        public void Set(int valX)
        {
            if (valX == 0f) return;
            lastNonZeroX = valX;
        }
    }
}