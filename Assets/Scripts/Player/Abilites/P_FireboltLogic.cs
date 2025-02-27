using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_FireboltLogic : MonoBehaviour
{
    private float maxDistanceFromPlayer = 20.0f;

    private int DAMAGE = 50; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileRangeCap();
    }

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
            collision.gameObject.GetComponent<E_HealthController>().TakeDamage(DAMAGE);
        }
        Destroy(gameObject);
    }
}
