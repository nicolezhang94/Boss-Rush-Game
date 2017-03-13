using UnityEngine;
using BossRush.Common;

public class BeeAttack : MonoBehaviour
{
    public AudioClip beeSound;
    AudioSource beeSoundSource;
    private Attack collisionAttack = new Attack { Damage = .1f, DamageType = DamageType.Normal };
    private Attack simonCollisionAttack = new Attack { Damage = .5f, DamageType = DamageType.Normal };
    private EnemyAttackManager enemyAttack;
    public EnemyHealth beeHealth;
    public bool isSimonBee = false;
    // Use this for initialization
    void Start()
    {
        enemyAttack = EnemyAttackManager.Instance;
        beeSoundSource = GetComponent<AudioSource>();
        beeHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (beeHealth.health <= 0)
        {
            Die();
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.gameObject.CompareTag("Player"))
    //    {
    //        enemyAttack.HitPlayer(collisionAttack);
    //        beeSoundSource.PlayOneShot(beeSound);

    //    }
    //}

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            if(isSimonBee == true)
            {
                enemyAttack.HitPlayer(simonCollisionAttack);
                beeSoundSource.PlayOneShot(beeSound);
            }
            else
            {
                enemyAttack.HitPlayer(collisionAttack);
                beeSoundSource.PlayOneShot(beeSound);
            }
            

        }
    }

    void Die()
    {
        //Do special things
        //Do default things
        beeHealth.Die();
        Destroy(gameObject);        
    }
}
