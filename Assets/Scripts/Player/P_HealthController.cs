using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_HealthController : HealthController
{
    public float MaxHealth
    {
        get { return maxHealth;}
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Player Takes Damage

        // Test For Now

       if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Projectile"))
        {
            if (gameObject.GetComponent<P_ThickSkinnedAbility>().isActive)
            {
                gameObject.GetComponent<P_ThickSkinnedAbility>().TakeDamage(10.0f);
            }
            else
            {
                TakeDamage(10.0f);
            }
        }
    }
}
