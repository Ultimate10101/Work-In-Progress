using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_InverseCureShotProjectileLogic : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<E_HealthController>().HealHealth(5.0f);
        }
    }
}
