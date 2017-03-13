using UnityEngine;
using BossRush.Common; //Add this to any file that needs access to common code
using System.Collections.Generic;

public class HiveAttack : MonoBehaviour
{
    public HiveMovement hiveMovement;
    void Start()
    {
        hiveMovement = GetComponent<HiveMovement>();
       
    }
    private Dictionary<string, Attack> BossAttacks = new Dictionary<string, Attack>()
    {
        {"collide", new Attack { DamageType = DamageType.Normal, Damage = 1, UseTime = 0.1f, CooldownTimer = new Timer(0.25f) }  }
    };

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.CompareTag("Player"))
        {
            EnemyAttackManager.Instance.HitPlayer(BossAttacks["collide"]);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.collider.gameObject.CompareTag("Player"))
        {
            EnemyAttackManager.Instance.HitPlayer(BossAttacks["collide"]);
            hiveMovement.moveSpeed = 0;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.gameObject.CompareTag("Player"))
        {
            hiveMovement.moveSpeed = 2;
        }
    }
}
