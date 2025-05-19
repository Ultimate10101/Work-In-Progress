using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_BOX_DAMAGE : MonoBehaviour
{

    private int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<P_ThickSkinnedAbility>().isActive)
            {
                other.gameObject.GetComponent<P_ThickSkinnedAbility>().TakeDamage(damage);
            }
            else
            {
                other.gameObject.GetComponent<P_HealthController>().TakeDamage(damage);
            }
        }
    }
}
