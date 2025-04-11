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

    private StatusEffectHandler playerStatus;

    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = P_PlayerController.playerControllerRef.gameObject.GetComponent<StatusEffectHandler>();

        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        FireInput();

        if (fireKey && canFire && playerStatus.currentStatusEffect != StatusEffectHandler.StatusEffects.STUNNED)
        {
            Firing();
        }

        //Fire_DTl();
    }


    void FireInput()
    {
        fireKey = Input.GetKey(KeyCode.Mouse0);
    }


    void Firing()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("metarig_ShootingAnim_True")) 
        { 
            anim.SetBool("IsFiring", true);
        }
        Debug.Log("Playing");

    }


    void Fire_DTl()
    {
        Debug.Log("I work");

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

        anim.SetBool("IsFiring", false);

        Debug.Log("I exit");

    }


    void ShotCounter()
    {
        canFire = true;
    }


}
