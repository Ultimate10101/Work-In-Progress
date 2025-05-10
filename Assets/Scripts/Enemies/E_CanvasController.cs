using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class E_CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject enemyCanvas;

    [SerializeField] private Camera gameCam;

    public bool isActive;

    private float timer;


    private void Start()
    {
        isActive = false;
    }

    void Update()
    {
        if (enemyCanvas.activeSelf)
        {
            HealthFacePlayer();
        }

        if(isActive)
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
    }

    public void TimeUntilDeactivation()
    {
        timer = Mathf.MoveTowards(timer, 0.0f, 1.0f * Time.deltaTime);

        if(timer <= 0.0f)
        {
            CanvasActive(false);
        }
    }

    void HealthFacePlayer()
    {
        Vector3 lookAt = new Vector3(gameCam.transform.position.x, enemyCanvas.transform.position.y, gameCam.transform.position.z);
        enemyCanvas.transform.LookAt(lookAt, Vector3.down);
    }
}
