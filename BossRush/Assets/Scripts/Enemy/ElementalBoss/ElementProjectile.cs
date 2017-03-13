using UnityEngine;
using BossRush.Common;
using System.Collections.Generic;

public class ElementProjectile : MonoBehaviour {

    //    public Mesh currentMesh;
    public Transform target;
    public ElementalBossController bossHealth;
    //    Transform aim;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void doAttack(DamageType element, Vector3 startLocation, Vector3 endLocation, float delta)
    {
        if (element == DamageType.Grass)
        {
            //            transform.Translate(0, 4.75f, 0);
            transform.position = Vector3.Lerp(transform.position, endLocation, delta);
            // up position attack should be at y = -1.5
            // down position should be at y = -6.25
        }
        else if (element == DamageType.Fire)
        {
            transform.position = Vector3.Lerp(transform.position, endLocation, delta);
        }
        else if (element == DamageType.Water)
        {
            transform.position = Vector3.Lerp(transform.position, endLocation, delta);
        }
    }

    public void resetAttack(DamageType element)
    {
        if (element == DamageType.Grass)
        {
//            transform.Translate(0, -4.75f, 0);
            transform.position = new Vector3(0.16742f, -6.25f, 6.3043f);
            // up position attack should be at y = -1.5
            // down position should be at y = -6.25
        }
        else if (element == DamageType.Fire)
        {
            //           transform.Translate(bossHealth.transform.position);
            transform.position = new Vector3(0.04f, -0.9f, 6.53f);
        }
        else if (element == DamageType.Water)
        {
//            transform.rotation = new Quaternion(0.0f, 0.0f, 180.0f, 1.0f);
            transform.position = new Vector3(0.1f, -1.6f, 6.5f);
        }
    }

    public void prepareAttack(DamageType element, Vector3 startLocation, Vector3 endLocation, float delta)
    {
        if (element == DamageType.Grass)
        {
            // rumbling sound
        }
        else if (element == DamageType.Fire)
        {
            transform.position = Vector3.Lerp(transform.position, endLocation, delta);
        }
        else if (element == DamageType.Water)
        {
            transform.position = Vector3.Lerp(transform.position, endLocation, delta);
        }
    }

    private Dictionary<string, Attack> ElementalAttacks = new Dictionary<string, Attack>()
    {
        {"collide", new Attack { DamageType = DamageType.Normal, Damage = 1, UseTime = 0.3f, CooldownTimer = new Timer(0.35f) }  },
        {"fire", new Attack { DamageType = DamageType.Fire, Damage = 1, UseTime = 0.3f, CooldownTimer = new Timer(0.35f) }  },
        {"water", new Attack { DamageType = DamageType.Water, Damage = 1, UseTime = 0.3f, CooldownTimer = new Timer(0.35f) }  },
        {"grass", new Attack { DamageType = DamageType.Grass, Damage = 1, UseTime = 0.3f, CooldownTimer = new Timer(0.35f) }  }
    };

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.CompareTag("Player"))
        {
            EnemyAttackManager.Instance.HitPlayer(ElementalAttacks["collide"]);
//            if ()
            //            EnemyAttackManager.Instance.HitPlayer(ElementalAttacks["fire"]);
        }
    }

    void OnTriggerEnter(Collision other)
    {
        if (other.collider.gameObject.CompareTag("Player"))
        {
            EnemyAttackManager.Instance.HitPlayer(ElementalAttacks["collide"]);
            //            if ()
            //            EnemyAttackManager.Instance.HitPlayer(ElementalAttacks["fire"]);
        }
    }

    void OnTriggerStay(Collision other)
    {
        if (other.collider.gameObject.CompareTag("Player"))
        {
            EnemyAttackManager.Instance.HitPlayer(ElementalAttacks["collide"]);
            //            if ()
            //            EnemyAttackManager.Instance.HitPlayer(ElementalAttacks["fire"]);
        }
    }

}
