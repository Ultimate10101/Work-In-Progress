using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class E_CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject enemyCanvas;

    [SerializeField] private Camera gameCam;

    public bool isActive;
    private bool timerActive;

    private float timer;


    private void Start()
    {
        isActive = false;

        timerActive = false;
    }

    void Update()
    {
        if (enemyCanvas.activeSelf)
        {
            HealthFacePlayer();
        }

        if(timerActive)
        {
            TimeUntilDeactivation();
        }
    }

    public void CanvasActive(bool activeStatus)
    {

      enemyCanvas.SetActive(activeStatus);
      isActive = activeStatus;

    }

    public void ActiveFor(int seconds)
    {
        timer = seconds;
        timerActive = true;
    }

    public void TimeUntilDeactivation()
    {
        timer = Mathf.MoveTowards(timer, 0.0f, 1.0f * Time.deltaTime);

        if(timer <= 0.0f)
        {
            CanvasActive(false);
            timerActive = false;
        }
    }

    void HealthFacePlayer()
    {
        Vector3 lookAt = new Vector3(gameCam.transform.position.x, enemyCanvas.transform.position.y, gameCam.transform.position.z);
        enemyCanvas.transform.LookAt(lookAt, Vector3.down);
    }
}
