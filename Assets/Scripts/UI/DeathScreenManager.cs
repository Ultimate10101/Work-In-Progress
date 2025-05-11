using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
    private Vector3 respawnPosition;

    void Update()
    {
        gameObject.SetActive(true);    
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0); // --> PlaceHolder for Buildindex
    }


    void Respawn()
    {
        // Set Player Back to the beginning and refresh enemies on map, but not puzzels
        P_PlayerController.playerControllerRef.transform.position = respawnPosition;

    }
}
