using BossRush.Common;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator anim;
	Timer attack = new Timer(0.5f);
	bool h = false;
    public AudioClip swoosh1;
    public AudioClip swoosh2;
    public AudioSource swordAudio;
    void Start()
    {
        anim = GetComponentInParent<Animator>();
		attack.reset ();
    }

    // Update is called once per frame
    void Update()
    {
		attack.update ();

		if (attack.isReady ()) {
			h = false;
		}

        Swing();
    }

    void OnTriggerEnter(Collider other)
    { 
        if (h && other.CompareTag("Hostile"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();

            if (enemy.elementType == DamageType.Normal)
            {
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
            else {
                DamageType swordElement = GetComponent<ElementalSword>().currentSwordElement;

                if (enemy.elementType == DamageType.Fire && swordElement == DamageType.Water)
                {
                    PlayerAttackManager.Instance.DoAttack("water", enemy);
                }
                else if (enemy.elementType == DamageType.Water && swordElement == DamageType.Grass)
                {
                    PlayerAttackManager.Instance.DoAttack("grass", enemy);
                }
                else if (enemy.elementType == DamageType.Grass && swordElement == DamageType.Fire)
                {
                    PlayerAttackManager.Instance.DoAttack("fire", enemy);
                }
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
            if((Input.GetKeyDown(GameManager.AttackKey) || Input.GetMouseButtonDown(0)))
            {
                cooldownTimer.reset();
                // I'M NOT SURE IF THIS IS A PROBLEM HERE BUT I WANTED TO MAKE A NOTE OF IT
                anim.SetTrigger("Attacking");
                attack.reset();
                h = true;
                var rando = Random.Range(0, 1);
                if (rando == 0)
                {
                    swordAudio.PlayOneShot(swoosh1);
                }
                if (rando == 1)
                {
                    swordAudio.PlayOneShot(swoosh2);
                }
                if (animFlag == 1)
                {
                    anim.SetInteger("AttackFlag", 2);
                }
                else if (animFlag == 2)
                {
                    anim.SetInteger("AttackFlag", 1);
                }
            }
        }
    }
}
