using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_PatrollerProjectileLogic : MonoBehaviour
{

    private int DAMAGE = 5;

    void Start()
    {
        Destroy(gameObject,1f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            if(collision.gameObject.GetComponent<P_ThickSkinnedAbility>().isActive)
            {
                collision.gameObject.GetComponent<P_ThickSkinnedAbility>().TakeDamage(DAMAGE);
            }
            else
            {
                collision.gameObject.GetComponent<P_HealthController>().TakeDamage(DAMAGE);
            }
        }
        Destroy(gameObject);
    }
}
