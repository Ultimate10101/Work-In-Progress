using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assess [information viewer] Menu Option for Dev Tool lite Menu

public class P_AssessFunction : MonoBehaviour
{
    [SerializeField] private Camera gameCam;

    private E_CanvasController enemyCanvas;

    private float launchTimeOffset;

    private float coolDown;

    private bool accessKey;

    private bool readyToActivate;

    private int duration;

    //AudioManager audioManager;


    void Start()
    {
        launchTimeOffset = 1.5f;

        coolDown = 0.5f;

        readyToActivate = true;

        duration = 18;

    }


    public void ActivateAssess()
    {
        accessKey = Input.GetKeyDown(KeyCode.T);
    }

    public void Assess(Animator playerAnim)
    {
        if (readyToActivate && accessKey)
        {
            Debug.Log("Casting");
            readyToActivate = false;
            //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();

            playerAnim.SetTrigger("IsAssessing");

            StartCoroutine(CastDelay(playerAnim));
        }
    }

    private IEnumerator CastDelay(Animator playerAnim)
    {
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length + launchTimeOffset);

        if (IsValidTarget())
        {
            enemyCanvas.InformationPanelActivate(true, duration);
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
            enemyCanvas = hit.collider.gameObject.GetComponent<E_CanvasController>();

            if (!enemyCanvas.isActive)
            {
                Debug.Log("Enemy Targeted with Assess");
                return true;
            }

            Debug.Log("Assess already active on this target");

            return false;
        }

        Debug.Log("Nothing was hit with Assess");
        return false;

    }


}
