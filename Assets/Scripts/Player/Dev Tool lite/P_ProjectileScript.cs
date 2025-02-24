using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_ProjectileScript : MonoBehaviour
{

    private float maxDistanceFromPlayer = 75.0f;

    public static bool enemyHit;

    private int DAMAGE = 2; 

    // Start is called before the first frame update
    void Start()
    {
        enemyHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileRangeCap();
    }

    // Fix projectile Range Cap
    void ProjectileRangeCap()
    {

        if (CalculateDistance() >= maxDistanceFromPlayer)
        {
            Destroy(gameObject);
        }
    }

    float CalculateDistance()
    {
        return Mathf.Sqrt(Mathf.Pow(transform.position.z - P_PlayerController.playerControllerRef.transform.position.z, 2) +
                    Mathf.Pow(transform.position.x - P_PlayerController.playerControllerRef.transform.position.x, 2) +
                    Mathf.Pow(transform.position.y - P_PlayerController.playerControllerRef.transform.position.y, 2));

    } 

    private void OnCollisionEnter(Collision collision)
    {
        // Update portion later, use for testing now

        // Projectile will be destroyed when hitting gameobject (enemy or environment)
        // if projectile hits enemy, mana will increase at a constant value
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyHit = true;
            collision.gameObject.GetComponent<E_HealthController>().TakeDamage(DAMAGE);
        }
        Destroy(gameObject);
    }

}
