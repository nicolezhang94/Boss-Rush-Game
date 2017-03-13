using UnityEngine;
using BossRush.Common; //Add this to any file that needs access to common code
using System.Collections.Generic;

public class PlayerAttackManager : Singleton<PlayerAttackManager> {
    protected PlayerAttackManager() { }

    public readonly Dictionary<string, Attack> PlayerAttacks = new Dictionary<string, Attack>()
    {
        {"sword", new Attack { DamageType = DamageType.Normal, Damage = 1, UseTime = 0.3f, CooldownTimer = new Timer(0.35f) }  }, //This is something we may be able to use resource files for. Dunno what best practices are.
        {"fire", new Attack { DamageType = DamageType.Fire, Damage = 1, UseTime = 0.3f, CooldownTimer = new Timer(0.35f) }  },
        {"water", new Attack { DamageType = DamageType.Water, Damage = 1, UseTime = 0.3f, CooldownTimer = new Timer(0.35f) }  },
        {"grass", new Attack { DamageType = DamageType.Grass, Damage = 1, UseTime = 0.3f, CooldownTimer = new Timer(0.35f) }  }
    };

	// Use this for initialization
	void Awake () {
        foreach (Attack attack in PlayerAttacks.Values)
        {
            attack.CooldownTimer.reset();
        }
    }
	
	// Update is called once per frame
	void Update () {
	    foreach(Attack attack in PlayerAttacks.Values)
        {
            attack.CooldownTimer.update();
        }
	}

    public void DoAttack(string attackName, EnemyHealth target)
    {
        Attack attack = PlayerAttacks[attackName];
        target.TakeDamage(attack);
    }
}
