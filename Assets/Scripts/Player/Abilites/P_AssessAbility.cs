using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assess [information viewer] Menu Option for Dev Tool lite Menu

public class P_AssessAbility : MonoBehaviour
{
    [SerializeField] private Camera gameCam;

    private E_CanvasController enemyCanvas;

    private float currentTarget;

    private float previousTarget;

    private float launchTimeOffset;

    private float coolDown;

    private bool accessKey;

    private bool readyToActivate;

    private int duration;


    void Start()
    {
        launchTimeOffset = 1.5f;

        coolDown = 0.5f;

        readyToActivate = true;

        duration = 18;

    }


    public void ActivateAssess()
    {
        accessKey = Input.GetKeyDown(KeyCode.G);
    }

    public void Assess(Animator playerAnim)
    {
        if (readyToActivate && accessKey)
        {
            Debug.Log("Casting");
            readyToActivate = false;

            playerAnim.SetTrigger("IsAssessing");

            StartCoroutine(CastDelay(playerAnim));
        }
    }

    private IEnumerator CastDelay(Animator playerAnim)
    {
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length + launchTimeOffset);

        if (IsValidTarget())
        {
            if (DurationBugFix())
            {
                enemyCanvas.InformationPanelActivate(true, duration);

                enemyCanvas = null;
                previousTarget = currentTarget;
            }

        }
        else
        {
            Debug.Log("Spell missed target");
        }


        StartCoroutine(CoolDownHandler());

        Debug.Log("Finished");
    }

    private IEnumerator CoolDownHandler()
    {
        yield return new WaitForSeconds(coolDown);
        readyToActivate = true;
        Debug.Log("Ready to cast again");
    }

    // Return true if Player is looking at Target the Spell is usuable on 
    bool IsValidTarget()
    {
        Ray ray = gameCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Targeted with Assess");
            enemyCanvas = hit.collider.gameObject.GetComponent<E_CanvasController>();

            currentTarget = enemyCanvas.GetInstanceID();

            return true;
        }

        Debug.Log("Nothing was hit with Assess");
        return false;

    }


    // If access ability is already active on target, spell can't be casted on them again
    bool DurationBugFix()
    {
        if (currentTarget == previousTarget)
        {
            if (enemyCanvas.isActive)
            {
                Debug.Log("Still Running");
                return false;
            }
            return true;
        }
        return true;
    }


}
