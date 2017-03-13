using UnityEngine;
using BossRush.Common;

public class EnemyAttackManager : Singleton<EnemyAttackManager>
{
    protected EnemyAttackManager() { }

    GameObject player;
    PlayerHealth playerHealth;

    public void HitPlayer(Attack attack)
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().TakeDamage(attack); ;
    }
}
