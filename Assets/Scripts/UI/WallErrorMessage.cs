using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallErrorMessage : MonoBehaviour
{
    private bool active;

   void Start()
    {
        active = false;
    }

    void Update()
    {
        if(gameObject.activeSelf && !active)
        {
            active = true;
            StartCoroutine(Deactivate());
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
        active = false;

    }
}
