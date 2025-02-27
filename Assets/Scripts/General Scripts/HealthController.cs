using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// General Control Script for Health

public class HealthController : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;

    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    protected LivingStatus creatureLivingStatus;


    void Start()
    {
        healthText.text = health+"/"+maxHealth;
        maxHealth = health;

        creatureLivingStatus = LivingStatus.ALIVE;
    }

    protected virtual void Update()
    {
        if (healthBar.IsActive()) 
        {
            UpdateHealthUI_Info();
        }

        Death();
    }

    void UpdateHealthUI_Info()
    {
        health = Mathf.Clamp(health, 0.0f, maxHealth);
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0.0f, 1.0f);
        healthText.text = health + "/" + maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void HealHealth(float health)
    {
        this.health += health;
    }

    public void Death()
    {
        if (health<=0.0f)
        {
            creatureLivingStatus = LivingStatus.DEAD;
        }
    }
}
