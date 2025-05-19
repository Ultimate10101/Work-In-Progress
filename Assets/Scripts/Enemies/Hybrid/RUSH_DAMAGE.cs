using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RUSH_DAMAGE : MonoBehaviour
{

    private int damage;
    private int SpecialDamage;

    public bool specialActivated;


    private void Start()
    {
        damage = 20;
        SpecialDamage = 40;
        specialActivated = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (other.gameObject.GetComponent<P_ThickSkinnedAbility>().isActive)
            {

                if (specialActivated)
                {
                    other.gameObject.GetComponent<P_ThickSkinnedAbility>().TakeDamage(SpecialDamage);
                }

                other.gameObject.GetComponent<P_ThickSkinnedAbility>().TakeDamage(damage);
            }
            else
            {
                if (specialActivated)
                {
                    other.gameObject.GetComponent<P_HealthController>().TakeDamage(SpecialDamage);
                }

                other.gameObject.GetComponent<P_HealthController>().TakeDamage(damage);
            }


            if (specialActivated)
            {
                other.gameObject.GetComponent<P_ManaController>().Mana -= 10;
            }
        }
    }


}
