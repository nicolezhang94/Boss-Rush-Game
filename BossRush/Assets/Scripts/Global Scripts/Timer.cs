using UnityEngine;

namespace BossRush.Common
{
    public class Timer
    {
        public float MaxTime { get; set; }
        public float CurrentTime { get; set; }
        public float LastTime { get; set; }

        protected Timer() { }

        public Timer(float max)
        {
            MaxTime = max;
            CurrentTime = 0;
        }

        public void update()
        {
            if (CurrentTime > 0f)
            {
                LastTime = CurrentTime;
                CurrentTime = Mathf.Max(0f, CurrentTime - Time.deltaTime);
            }
        }

        public bool isReady()
        {
            return CurrentTime == 0;
        }

        public void reset()
        {
            CurrentTime = MaxTime;
        }
    }
}
