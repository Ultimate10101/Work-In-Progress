using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateMessage : MonoBehaviour
{
    private bool isActive;

    private void Start()
    {
        isActive = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && !isActive)
        {
            isActive = true;
            StartCoroutine(Deactivate());
        }
    }


    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(5.0f);
        gameObject.SetActive(false);
    }
}
