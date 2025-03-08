using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastEffectTest : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private float radius = 0.0f;

    private int pointCount = 30;

    private float angleBetweenPoints = 0.0f;

    private float speed = 10.0f;

    private float maxRadius = 10.0f;

    private Vector3[] positions = new Vector3[30];

    private Collider[] col;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.enabled = true;
        lineRenderer.positionCount = pointCount;


        FindPoints();


    }

    // Update is called once per frame
    void Update()
    {
        if(radius <= maxRadius)
        {
            SetPositions();

            radius += Time.deltaTime * speed;

            lineRenderer.widthMultiplier = (maxRadius - radius) / maxRadius;
        }
        else
        { 
            Destroy(gameObject);
        }
    }


    void FindPoints()
    {
        angleBetweenPoints = 360 / pointCount;

        for (int i = 0; i < pointCount; i++)
        {
            float angle = angleBetweenPoints * i * Mathf.Deg2Rad;
            positions[i] = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
        }
        positions[pointCount - 1] = positions[0];
    }

    void SetPositions()
    {
        for (int i = 0;i < pointCount; i++)
        {
            lineRenderer.SetPosition(i, positions[i] * radius);
        }
    }

    void FindTargets()
    {
        col = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider target in col)
        {
            if(target.gameObject.CompareTag("Player"))
            {
                //STUN PLAYER
            }
        }
    }
}
