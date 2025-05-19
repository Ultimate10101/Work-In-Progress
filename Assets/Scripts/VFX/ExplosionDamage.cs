using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    private Collider[] col;

    // Start is called before the first frame update
    void Start()
    {
        col = Physics.OverlapSphere(transform.position, 7.0f);

        foreach(Collider c in col)
        {
            if(c.gameObject.GetComponent<E_HealthController>() != null)
            {
                if (c.gameObject.GetComponent<E_AIMovement>().currentState == EnemyState.PATROLLING)
                {
                    c.gameObject.GetComponent<E_AIMovement>().wasHit = true;
                }
                c.gameObject.GetComponent<E_HealthController>().TakeDamage(20.0f);
            }
        }
    }
}
