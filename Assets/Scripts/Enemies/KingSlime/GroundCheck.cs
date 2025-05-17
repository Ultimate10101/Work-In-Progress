using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool hitGround;

    public bool HitGround
    {
        get { return hitGround; }
        set { hitGround = value; }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            hitGround = true;
        }
    }

}
