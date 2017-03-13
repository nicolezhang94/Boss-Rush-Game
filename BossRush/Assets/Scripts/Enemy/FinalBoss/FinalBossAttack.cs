using UnityEngine;
using System.Collections.Generic;

namespace BossRush.Common
{
    public class FinalBossAttack : MonoBehaviour
    {
        private Dictionary<string, Attack> BossAttacks = new Dictionary<string, Attack>()
        {
            {"collide", new Attack { DamageType = DamageType.Normal, Damage = 999, UseTime = 0.1f, CooldownTimer = new Timer(0f) }  }
        };

        void OnCollisionEnter(Collision other)
        {
            if (other.collider.gameObject.CompareTag("Player"))
            {
                EnemyAttackManager.Instance.HitPlayer(BossAttacks["collide"]);
            }
        }
    }
}
