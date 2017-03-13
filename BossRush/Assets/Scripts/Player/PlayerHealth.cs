using UnityEngine;
using BossRush.Common;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    static float health;
    public float maxHealth = 10;
    public float iFrames = 0.25f;
    Timer iFrameTimer;
    RectTransform healthBar;
    float barWidth;
    Renderer m_renderer;

    // Use this for initialization
    void Awake () {
        iFrameTimer = new Timer(iFrames);
        health = maxHealth;
    }

    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<RectTransform>();
        barWidth = healthBar.sizeDelta.x;
        m_renderer = gameObject.GetComponentInChildren<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        iFrameTimer.update();
        //Placeholder to test damage
        if (Input.GetKeyDown(KeyCode.Z) && health > 0)
        {
            TakeDamage(new Attack { DamageType = DamageType.Normal, Damage = 10, UseTime = 0.1f, CooldownTimer = new Timer(0.25f) });
        }
	}

    public void TakeDamage(Attack attack)
    {
        if (iFrameTimer.isReady())
        {
            iFrameTimer.reset();
            health -= attack.Damage;
            Vector2 size = healthBar.sizeDelta;
            size.x = (float)health * barWidth / maxHealth;
            healthBar.sizeDelta = size;
            StartCoroutine("ChangeColorOnHit");
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (SceneManager.GetActiveScene().name.Equals("FinalBoss"))
        {
            GameManager.FinalBossAttempt.Add(new LastBossAction(Time.timeSinceLevelLoad, FinalBossFlag.Die));
            GameManager.LogFinalBossAttempt();
        }

        Debug.Log("ded");
        GameManager.ReloadSceneOnDeath();
    }

    IEnumerator ChangeColorOnHit()
    {
        var prevColor = m_renderer.material.color;
        m_renderer.material.color = Color.red;
        yield return new WaitForSeconds(iFrames);
        m_renderer.material.color = prevColor;
    }
}
