using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_FireboltLogic : MonoBehaviour
{
    private float maxDistanceFromPlayer = 70.0f;


    private int damage = 30;

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

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!P_DTLMenu.DTLMenuRef.Inverse)
            {
                collision.gameObject.GetComponent<E_HealthController>().TakeDamage(DAMAGE);
            }
            else
            {
                collision.gameObject.GetComponent<E_HealthController>().TakeDamage(INVERSE_DAMAGE);

                P_PlayerController.playerControllerRef.gameObject.GetComponent<P_ManaController>().ManaIncrease();

            }

        }
        Destroy(gameObject);
    }
}
