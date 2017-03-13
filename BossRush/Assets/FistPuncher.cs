using UnityEngine;
using System.Collections;
using BossRush.Common;
using System.Collections.Generic;

public class FistPuncher : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
