using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject enemyCanvas;

    [SerializeField] private Camera gameCam;

    public bool isActive;

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
    }

    public void CanvasActive(bool activeStatus)
    {

      enemyCanvas.SetActive(activeStatus);
      isActive = activeStatus;

    }

    public IEnumerator ActiveFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        CanvasActive(false);

    }

    void HealthFacePlayer()
    {
        enemyCanvas.transform.LookAt(gameCam.transform.position, Vector3.down);
    }
}
