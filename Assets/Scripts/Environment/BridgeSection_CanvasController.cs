using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSection_CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject bridgeCanvas;

    public bool isActive;

    private float timer;


    private void Start()
    {
        bridgeCanvas.SetActive(false);
        isActive = false;
    }

    private void Update()
    {
        FacePlayer();

        if (isActive)
        {
            TimeUntilDeactivation();
        }
    }

    public void BridgePanelActivate(bool activeStatus, int seconds)
    {

        bridgeCanvas.SetActive(activeStatus);
        isActive = activeStatus;

        timer = seconds;
    }


    public void TimeUntilDeactivation()
    {
        timer = Mathf.MoveTowards(timer, 0.0f, 1.0f * Time.deltaTime);

        if (timer <= 0.0f)
        {
            BridgePanelActivate(false, 0);
        }
    }

    private void FacePlayer()
    {
        bridgeCanvas.transform.rotation = Quaternion.LookRotation(bridgeCanvas.transform.position - P_PlayerController.playerControllerRef.transform.position);

    }
}
