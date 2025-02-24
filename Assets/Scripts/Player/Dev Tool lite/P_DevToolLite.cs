using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DevToolLite : MonoBehaviour
{
    [SerializeField] private Camera gameCam;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shootForce;

    private bool fireKey;
    [SerializeField] private bool canFire;

    [SerializeField] private float timeBetweenShots;

    

    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        FireInput();

        Fire();
    }


    void FireInput()
    {
        fireKey = Input.GetKey(KeyCode.Mouse0);
    }


    void Fire()
    {
        if (fireKey && canFire)
        {
            canFire = false;

            // ray through middle of screen
            Ray ray = gameCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;


            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(100.0f); // Point far away
            }

            Vector3 shootDir = targetPoint - attackPoint.position;

            // Quaternion.identity -->  "no rotation" - the object is perfectly aligned with the world or parent axes
            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);

            projectile.transform.forward = shootDir.normalized;

            projectile.GetComponent<Rigidbody>().AddForce(shootDir.normalized * shootForce, ForceMode.Impulse);

            Invoke("ShotCounter", timeBetweenShots);

        }
    }

    void ShotCounter()
    {
        canFire = true;
    }


}
