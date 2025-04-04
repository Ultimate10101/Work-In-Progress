using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RUSH_DAMAGE : MonoBehaviour
{

    public bool specialActivated;


    private void Start()
    {
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
                    other.gameObject.GetComponent<P_ThickSkinnedAbility>().TakeDamage(40);
                }

                other.gameObject.GetComponent<P_ThickSkinnedAbility>().TakeDamage(20);
            }
            else
            {
                if (specialActivated)
                {
                    other.gameObject.GetComponent<P_HealthController>().TakeDamage(40);
                }

                other.gameObject.GetComponent<P_HealthController>().TakeDamage(20);
            }


            if (specialActivated)
            {
                other.gameObject.GetComponent<P_ManaController>().Mana -= 5;
            }
        }
    }


}
