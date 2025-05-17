using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_FireboltLogic : MonoBehaviour
{
    private float maxDistanceFromPlayer = 70.0f;

    [SerializeField] private GameObject explosion;


    private int damage = 40;

    public int DAMAGE
    {
        get { return damage; }

        set { damage = value; }
    }


    private int inverseDamage = 5;

    public int INVERSE_DAMAGE
    {
        get { return inverseDamage; }

        set { inverseDamage = value; }
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

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!P_DTLMenu.DTLMenuRef.Inverse)
            {
                collision.gameObject.GetComponent<E_HealthController>().TakeDamage(DAMAGE);
                collision.gameObject.GetComponent<BurnDamage>().ApplyTicks(5, 1.0f, 5);
            }
            else
            {
                collision.gameObject.GetComponent<E_HealthController>().TakeDamage(INVERSE_DAMAGE);

                P_PlayerController.playerControllerRef.gameObject.GetComponent<P_ManaController>().ManaIncrease();

            }

            if (collision.gameObject.GetComponent<E_AIMovement>().currentState == EnemyState.PATROLLING)
            {
                collision.gameObject.GetComponent<E_AIMovement>().wasHit = true;
            }

        }

        if (!P_DTLMenu.DTLMenuRef.Inverse)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
