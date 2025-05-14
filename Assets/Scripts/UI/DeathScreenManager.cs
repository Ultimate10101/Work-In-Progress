using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
    private Vector3 respawnPosition;

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }


    public void Respawn()
    {
        // Set Player Back to the beginning and refresh enemies on map, but not puzzels
        //P_PlayerController.playerControllerRef.transform.position = respawnPosition;
        SceneManager.LoadScene("3D_GameScene");

    }
}
