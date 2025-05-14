using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictionWalls : MonoBehaviour
{
    [SerializeField] private GameObject errorMessage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            errorMessage.SetActive(true);
        }
    }

}
