using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_ProjectileLogic : MonoBehaviour
{

    private float maxDistanceFromPlayer = 30.0f;

    private int DAMAGE = 5; 


    // Update is called once per frame
    void Update()
    {
        ProjectileRangeCap();
    }

    // Fix projectile Range Cap
    void ProjectileRangeCap()
    {

        if (Vector3.Distance(gameObject.transform.position, P_PlayerController.playerControllerRef.transform.position) >= maxDistanceFromPlayer)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Update portion later, use for testing now

        // Projectile will be destroyed when hitting gameobject (enemy or environment)
        // if projectile hits enemy, mana will increase at a constant value
        if (collision.gameObject.CompareTag("Enemy"))
        {

            P_PlayerController.playerControllerRef.gameObject.GetComponent<P_ManaController>().ManaIncrease();

            collision.gameObject.GetComponent<E_HealthController>().TakeDamage(DAMAGE);

            if(collision.gameObject.GetComponent<E_AIMovement>().currentState == EnemyState.PATROLLING)
            {
                collision.gameObject.GetComponent<E_AIMovement>().wasHit = true;
            }    
        }
        Destroy(gameObject);
    }

}
