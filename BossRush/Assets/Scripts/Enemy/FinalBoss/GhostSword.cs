using BossRush.Common;
using UnityEngine;

public class GhostSword : MonoBehaviour
{
    Animator anim;
    Timer attack = new Timer(0.25f);
    bool h;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        attack.reset();
    }

    // Update is called once per frame
    void Update()
    {
        attack.update();

        if (attack.isReady())
        {
            h = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (h && other.CompareTag("Hostile"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            PlayerAttackManager.Instance.DoAttack("sword", enemy);

            //Knock back
            if (enemy.canBeKnockedBack)
            {
                var playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                var enemyPos = enemy.gameObject.transform.position;
                var direction = (playerPos - enemyPos).normalized;
                enemy.gameObject.GetComponent<Rigidbody>().AddForce(2 * direction * enemy.GetComponent<Rigidbody>().mass, ForceMode.Impulse); // This is a WIP
            }
        }
    }

    public void Swing()
    {
        //Swing sword
        Timer cooldownTimer = PlayerAttackManager.Instance.PlayerAttacks["sword"].CooldownTimer;
        int animFlag = anim.GetInteger("AttackFlag");

        if (cooldownTimer.isReady())
        {
            cooldownTimer.reset();
            anim.SetTrigger("Attacking");
            if (animFlag == 1)
            {
                anim.SetInteger("AttackFlag", 2);
            }
            else if (animFlag == 2)
            {
                anim.SetInteger("AttackFlag", 3);
            }
            else if (animFlag == 3)
            {
                anim.SetInteger("AttackFlag", 1);
            }
        }
    }
}
