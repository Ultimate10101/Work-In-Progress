using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_PatrollerProjectileLogic : MonoBehaviour
{

    private int damage;

    void Start()
    {
        damage = 10;
        Destroy(gameObject,1f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            if(collision.gameObject.GetComponent<P_ThickSkinnedAbility>().isActive)
            {
                collision.gameObject.GetComponent<P_ThickSkinnedAbility>().TakeDamage(damage);
            }
            else
            {
                collision.gameObject.GetComponent<P_HealthController>().TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
