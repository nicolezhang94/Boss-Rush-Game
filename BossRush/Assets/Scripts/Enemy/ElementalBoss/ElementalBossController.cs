using UnityEngine;
using BossRush.Common;
using System.Collections.Generic;

public class ElementalBossController : MonoBehaviour {

    public GameObject player;
//    public GameObject projectile;
    public Rigidbody projectile;
    public Material ElementFire, ElementWater, ElementGrass;
    public EnemyHealth bossHealth;
    public AudioSource elementalAudio;
    public AudioClip fireAttack;
    public AudioClip waterAttack;
    public AudioClip grassAttack;
    public Transform target;
    public float rotateSpeed = 4.0f;

    private Quaternion look;
    private Vector3 direction;

    Vector3 startLocation;
    Vector3 endLocation;

    Vector3 start1, start2, start3, start4, start5;

    float elementChangeInterval;
    Renderer m_renderer;
    Timer attackWait;

    float prepTime = 2.0f;
    float attackTime = 10.0f;

    float startTime;
    float attackDistance;

    bool isAttacking;
    bool isPreparing;

    // Use this for initialization
    void Start () {
//        player = GameObject.Find("Player").transform;
        elementChangeInterval = Random.Range(3.5f, 4.5f);
        m_renderer = gameObject.GetComponent<Renderer>();

        startTime = Time.time;

        isAttacking = false;
        isPreparing = false;
        attackWait = new Timer(2.5f);

        bossHealth = GetComponent<EnemyHealth>();

        bossHealth.elementType = DamageType.Grass;
        startLocation = new Vector3(0.16742f, -6.25f, 6.3043f);
        endLocation = new Vector3(0.16742f, -1.5f, 6.3043f);
        attackDistance = Vector3.Distance(startLocation, endLocation);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Destroy(gameObject);
            Die();
        }

		if(bossHealth.IsDead()){
			Die ();
		}

        direction = (target.position - transform.position).normalized;
        look = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * rotateSpeed);

        attackWait.update();

        elementChangeInterval -= Time.deltaTime;
        if (elementChangeInterval < 0)
        {
            if (bossHealth.elementType == DamageType.Grass)
            {
                GameObject.Find("BossElementGrass").GetComponent<ElementProjectile>().resetAttack(bossHealth.elementType);
            }
            else if (bossHealth.elementType == DamageType.Fire)
            {
                GameObject.Find("BossElementFire1").GetComponent<ElementProjectile>().resetAttack(bossHealth.elementType);
                GameObject.Find("BossElementFire2").GetComponent<ElementProjectile>().resetAttack(bossHealth.elementType);
                GameObject.Find("BossElementFire3").GetComponent<ElementProjectile>().resetAttack(bossHealth.elementType);
                GameObject.Find("BossElementFire4").GetComponent<ElementProjectile>().resetAttack(bossHealth.elementType);
                GameObject.Find("BossElementFire5").GetComponent<ElementProjectile>().resetAttack(bossHealth.elementType);
            }
            else if (bossHealth.elementType == DamageType.Water)
            {
                GameObject.Find("BossElementWater").GetComponent<ElementProjectile>().resetAttack(bossHealth.elementType);
            }
            //            elementChangeInterval = Random.Range(3.5f, 4.5f);
            elementChangeInterval = 4.0f;
            int rndElement = Random.Range(1, 4);
            switch (rndElement)
            {
                case 1:
                    bossHealth.elementType = DamageType.Fire;
                    m_renderer.material = ElementFire;
                    startLocation = new Vector3(0.04f, -0.9f, 6.53f);
                    start1 = new Vector3(2.4f, 2.1f, 6.5f);
                    start2 = new Vector3(1.5f, 3.6f, 6.5f);
                    start3 = new Vector3(0.0f, 4.2f, 6.5f);
                    start4 = new Vector3(-1.5f, 3.6f, 6.5f);
                    start5 = new Vector3(-2.4f, 2.1f, 6.5f);
                    endLocation = new Vector3(2.4f, 2.1f, 6.5f);
                    attackDistance = Vector3.Distance(startLocation, endLocation);
                    break;
                case 2:
                    bossHealth.elementType = DamageType.Water;
                    startLocation = new Vector3(0.1f, -1.6f, 6.5f);
                    endLocation = new Vector3(0.1f, 11.0f, 6.5f);
                    attackDistance = Vector3.Distance(startLocation, endLocation);
                    m_renderer.material = ElementWater;
                    break;
                case 3:
                    bossHealth.elementType = DamageType.Grass;
                    m_renderer.material = ElementGrass;
                    startLocation = new Vector3(0.16742f, -6.25f, 6.3043f);
                    endLocation = new Vector3(0.16742f, -1.5f, 6.3043f);
                    attackDistance = Vector3.Distance(startLocation, endLocation);
                    break;
            }
            isPreparing = true;
            isAttacking = false;
            attackWait.reset();
            startTime = Time.time;
        }

        if (isPreparing && !attackWait.isReady())
        {
            if (bossHealth.elementType == DamageType.Grass)
            {
                float dist = (Time.time - startTime) * prepTime;
                float journey = dist / attackDistance;
                GameObject.Find("BossElementGrass").GetComponent<ElementProjectile>().prepareAttack(bossHealth.elementType, startLocation, endLocation, journey);
            }
            else if (bossHealth.elementType == DamageType.Fire)
            {
                float dist = (Time.time - startTime) * prepTime;
                float journey = dist / attackDistance;
                GameObject.Find("BossElementFire1").GetComponent<ElementProjectile>().prepareAttack(bossHealth.elementType, startLocation, start1, journey);
                GameObject.Find("BossElementFire2").GetComponent<ElementProjectile>().prepareAttack(bossHealth.elementType, startLocation, start2, journey);
                GameObject.Find("BossElementFire3").GetComponent<ElementProjectile>().prepareAttack(bossHealth.elementType, startLocation, start3, journey);
                GameObject.Find("BossElementFire4").GetComponent<ElementProjectile>().prepareAttack(bossHealth.elementType, startLocation, start4, journey);
                GameObject.Find("BossElementFire5").GetComponent<ElementProjectile>().prepareAttack(bossHealth.elementType, startLocation, start5, journey);
            }
            else if (bossHealth.elementType == DamageType.Water)
            {
                float dist = (Time.time - startTime ) * prepTime;
                float journey = dist / attackDistance;
                GameObject.Find("BossElementWater").GetComponent<ElementProjectile>().prepareAttack(bossHealth.elementType, startLocation, endLocation, journey);
            }
        }

        if (attackWait.isReady() && !isPreparing)
        {
            if (bossHealth.elementType == DamageType.Grass)
            {
                float dist = (Time.time - startTime) * attackTime;
                float journey = dist / attackDistance;
                GameObject.Find("BossElementGrass").GetComponent<ElementProjectile>().doAttack(bossHealth.elementType, startLocation, endLocation, journey);
            }
            else if (bossHealth.elementType == DamageType.Fire)
            {
                float dist = (Time.time - startTime) * attackTime;
                float journey = dist / attackDistance;
                GameObject.Find("BossElementFire1").GetComponent<ElementProjectile>().doAttack(bossHealth.elementType, start1, endLocation, journey);
                GameObject.Find("BossElementFire2").GetComponent<ElementProjectile>().doAttack(bossHealth.elementType, start2, endLocation, journey);
                GameObject.Find("BossElementFire3").GetComponent<ElementProjectile>().doAttack(bossHealth.elementType, start3, endLocation, journey);
                GameObject.Find("BossElementFire4").GetComponent<ElementProjectile>().doAttack(bossHealth.elementType, start4, endLocation, journey);
                GameObject.Find("BossElementFire5").GetComponent<ElementProjectile>().doAttack(bossHealth.elementType, start5, endLocation, journey);
            }
            else if (bossHealth.elementType == DamageType.Water)
            {
                float dist = (Time.time - startTime) * attackTime;
                float journey = dist / attackDistance;
                GameObject.Find("BossElementWater").GetComponent<ElementProjectile>().doAttack(bossHealth.elementType, startLocation, endLocation, journey);
            }
        }

        if (isPreparing && attackWait.isReady())
        {
            if (bossHealth.elementType == DamageType.Grass)
            {
                elementalAudio.PlayOneShot(grassAttack);
                startLocation = new Vector3(0.16742f, -6.25f, 6.3043f);
                endLocation = new Vector3(0.16742f, -1.5f, 6.3043f);
                attackDistance = Vector3.Distance(startLocation, endLocation);
            }
            else if (bossHealth.elementType == DamageType.Fire)
            {
                elementalAudio.PlayOneShot(fireAttack);
                start1 = new Vector3(2.4f, 2.1f, 6.5f);
                start2 = new Vector3(1.5f, 3.6f, 6.5f);
                start3 = new Vector3(0.0f, 4.2f, 6.5f);
                start4 = new Vector3(-1.5f, 3.6f, 6.5f);
                start5 = new Vector3(-2.4f, 2.1f, 6.5f);
                startLocation = new Vector3(0.0f, 4.2f, 6.5f);
                endLocation = target.transform.position;
                attackDistance = Vector3.Distance(startLocation, endLocation);
            }
            else if (bossHealth.elementType == DamageType.Water)
            {
                elementalAudio.PlayOneShot(waterAttack);
                startLocation = new Vector3(0.1f, 11.0f, 6.5f);
                endLocation = target.transform.position;
                attackDistance = Vector3.Distance(startLocation, endLocation);
            }
            isPreparing = false;
            startTime = Time.time;
        }
    }

    void Die()
    {
        Debug.Log("rip elemental boss");
        GameObject.Find("Door").SetActive(false);
        GameStateManager.GreenJewelBossDead = true;

        bossHealth.Die();
    }
}
