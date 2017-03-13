using System;

namespace BossRush.Common
{
    [Flags]
    public enum FinalBossFlag
    {
        None = 0,
        W = 0x100,
        A = 0x200,
        S = 0x400,
        D = 0x800,
        Roll = 0x1000,
        Attack = 0x2000,
        Die = 0x4000
    }
    public static class FlagHelper
    {
        public static bool IsSet(FinalBossFlag mask, FinalBossFlag flag)
        {
            return (mask & flag) != FinalBossFlag.None;
        }
    }
}
