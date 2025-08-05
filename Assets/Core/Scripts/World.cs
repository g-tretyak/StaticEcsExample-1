using FFS.Libraries.StaticEcs;
using ZeroZeroGames.Ecs.Modules.Shared.Data;

namespace ZeroZeroGames.Ecs.Modules.Shared.World
{
    public struct WT : IWorldType
    {
    }

    public struct BaseSystemsType : ISystemsType
    {
    }

    public struct UnscaledSystemsType : ISystemsType
    {
    }

    public struct FixedSystemsType : ISystemsType
    {
    }

    public struct LateSystemsType : ISystemsType
    {
    }

    public abstract class W : World<WT>
    {
    }


    public static class Cxt
    {
        public static RuntimeData R => W.Context<RuntimeData>.Get();
        public static StaticData S => W.Context<StaticData>.Get();
    }

    public static class Sys
    {
        public abstract class B : W.Systems<BaseSystemsType>
        {
        }

        public abstract class U : W.Systems<UnscaledSystemsType>
        {
        }

        public abstract class L : W.Systems<LateSystemsType>
        {
        }

        public abstract class F : W.Systems<FixedSystemsType>
        {
        }
    }
}