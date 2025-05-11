using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class E_CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject informationPanel;

    [SerializeField] private GameObject enemyCanvas;

    [SerializeField] private GameObject burningIcon;
    [SerializeField] private GameObject dot_Icon;
    [SerializeField] private GameObject StunnedIcon;
    [SerializeField] private GameObject increasedDamageIcon;

    [SerializeField] private Camera gameCam;

    private StatusEffectHandler enemyStatus;

    public bool isActive;

    private float timer;


    private void Start()
    {
        isActive = false;

        enemyStatus = gameObject.GetComponent<StatusEffectHandler>();
    }

    private void Update()
    {
        FacePlayer();

        UpdateIcons();

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

    private void FacePlayer()
    {
        enemyCanvas.transform.rotation = Quaternion.LookRotation(enemyCanvas.transform.position - gameCam.transform.position);

    }


    private void UpdateIcons()
    {
        burningIcon.SetActive(enemyStatus.GetState("BURNING"));
        Debug.Log(enemyStatus.GetState("BURNING"));
        dot_Icon.SetActive(enemyStatus.GetState("HIT_BY_INVERSE_RESTORATION"));
        StunnedIcon.SetActive(enemyStatus.GetState("STUNNED"));
        increasedDamageIcon.SetActive(enemyStatus.GetState("DAMAGE_TAKEN_INCREASED")); ;

    }
}
