using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single); 
    }


    public void Respawn()
    {
        SceneManager.LoadScene("3D_GameScene", LoadSceneMode.Single);

    }
}
