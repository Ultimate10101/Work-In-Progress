using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCOntroller : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Rigidbody2D slimeRb;
    // Start is called before the first frame update
    void Start()
    {
        slimeRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("run");
        slimeRb.AddForce((player.transform.position - gameObject.transform.position).normalized * 3.0f, ForceMode2D.Force);
    }
}
