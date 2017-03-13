using System;

namespace BossRush.Common
{
    [Serializable]
    public class LastBossAction
    {
        public float Time { get; set; }
        public FinalBossFlag Mask { get; set; }

        protected LastBossAction(){}

        public LastBossAction(float time, FinalBossFlag mask)
        {
            Time = time;
            Mask = mask;
        }
    }
}
