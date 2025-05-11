using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class E_CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject informationPanel;

    [SerializeField] private GameObject healthBarBG;

    [SerializeField] private Camera gameCam;

    public bool isActive;

    private float timer;


    private void Start()
    {
        isActive = false;
    }

    void Update()
    {
        FacePlayer();

        if (isActive)
        {
            TimeUntilDeactivation();
        }
    }

    public void InformationPanelActivate(bool activeStatus, int seconds)
    {

        informationPanel.SetActive(activeStatus);
        isActive = activeStatus;

        timer = seconds;
    }


    public void TimeUntilDeactivation()
    {
        timer = Mathf.MoveTowards(timer, 0.0f, 1.0f * Time.deltaTime);

        if (timer <= 0.0f)
        {
            InformationPanelActivate(false, 0);
        }
    }

    void FacePlayer()
    {
        if (informationPanel.activeSelf)
        {
            informationPanel.transform.rotation = Quaternion.LookRotation(informationPanel.transform.position - gameCam.transform.position);
        }

        healthBarBG.transform.rotation = Quaternion.LookRotation(healthBarBG.transform.position - gameCam.transform.position);

    }
}
