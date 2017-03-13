using UnityEngine;
using BossRush.Common;
using System.Collections.Generic;

public class ElementalSword : MonoBehaviour
{
    public DamageType currentSwordElement = DamageType.Normal;
    public Material ElementFire, ElementWater, ElementGrass, Sword;
    private bool bossIsDead;
    Renderer m_renderer;

    void Start()
    {
        m_renderer = gameObject.GetComponent<Renderer>();
        bossIsDead = GameObject.Find("ElementalBoss").GetComponent<EnemyHealth>().IsDead();
    }

    void Update()
    {
        if (bossIsDead)
        {
            currentSwordElement = DamageType.Normal;
            m_renderer.material = Sword;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!bossIsDead)
        {
            if (other.CompareTag("Fire"))
            {
                currentSwordElement = DamageType.Fire;
                m_renderer.material = ElementFire;
            }
            else if (other.CompareTag("Water"))
            {
                currentSwordElement = DamageType.Water;
                m_renderer.material = ElementWater;
            }
            else if (other.CompareTag("Grass"))
            {
                currentSwordElement = DamageType.Grass;
                m_renderer.material = ElementGrass;
            }
            else if (other.CompareTag("Hostile"))
            {

            }
        }
    }
}