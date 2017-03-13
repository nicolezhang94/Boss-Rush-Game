using UnityEngine;

namespace BossRush.Common
{
    class FinalBossSegment
    {
        public Transform transform { get; set; }
        public Vector3 goal { get; set; }

        protected FinalBossSegment() { }

        public FinalBossSegment(Transform tr, Vector3 g)
        {
            transform = tr;
            goal = g;
        }

        public bool HasReachedGoal()
        {
            return (transform.position - goal).magnitude < 0.1f;
        }
    }
}
