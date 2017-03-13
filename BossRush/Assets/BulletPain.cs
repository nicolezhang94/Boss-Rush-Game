using UnityEngine;
using System.Collections;
using BossRush.Common;
using System.Collections.Generic;

public class BulletPain : MonoBehaviour {

    Timer lifespan = new Timer(3.0f);
    
    // Use this for initialization
	void Start () {
        lifespan.reset();
	}
	
	// Update is called once per frame
	void Update () {
        lifespan.update();
        if (lifespan.isReady())
        {
            Destroy(gameObject);
        }
	}

	private Dictionary<string, Attack> BossAttacks = new Dictionary<string, Attack>()

	{
		{"collide", new Attack { DamageType = DamageType.Normal, Damage = 1, UseTime = 0.1f, CooldownTimer = new Timer(0.25f) }  }
	};

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			EnemyAttackManager.Instance.HitPlayer(BossAttacks["collide"]);
            Destroy(gameObject);
        }
    }
}
