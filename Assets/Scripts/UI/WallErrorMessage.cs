using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallErrorMessage : MonoBehaviour
{
    private bool activate;

   void Start()
    {
        activate = false;
    }

    void Update()
    {
        if(gameObject.activeSelf && !activate)
        {
            activate = true;
            StartCoroutine(Deactivate());
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
        activate = false;

    }
}
