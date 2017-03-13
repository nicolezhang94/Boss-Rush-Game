using UnityEngine;
using BossRush.Common;
using System.Collections;

public class EnemyHealth: MonoBehaviour
{
    public float health = 10;
    public bool canBeKnockedBack;
    public float iFrames = 0.25f;
    public DamageType elementType;      // This is DamageType.Normal for all bosses except Elemental
    public Animation deathAnimation = null;
    public AudioClip deathSound = null;
    public AudioClip damageSound = null;
    public AudioSource audioSource = null;
    Timer iFrameTimer;
    Renderer m_renderer;
    DamageType weakness;

    // Use this for initialization
    void Start()
    {
        iFrameTimer = new Timer(iFrames);
        m_renderer = gameObject.GetComponentInChildren<Renderer>();
        elementType = DamageType.Normal;
}

    // Update is called once per frame
    void Update()
    {
        iFrameTimer.update();
    }

    public float TakeDamage(Attack attack)
    {
        if (iFrameTimer.isReady())
        {
            iFrameTimer.reset();
            health -= attack.Damage;
            if(damageSound != null)
            {
                audioSource.PlayOneShot(damageSound);
            }
            StartCoroutine("ChangeColorOnHit");
        }

        return health;
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void Die()
    {
        if(deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        //deathAnimation.Play();
        Destroy(gameObject);
    }

    IEnumerator ChangeColorOnHit()
    {
        var prevColor = m_renderer.material.color;
        m_renderer.material.color = Color.red;
        yield return new WaitForSeconds(iFrames);
        m_renderer.material.color = prevColor;
    }
}
