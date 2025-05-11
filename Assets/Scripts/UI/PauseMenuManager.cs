using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    void Update()
    {
        PauseMenu();
    }

    void PauseMenu()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0); // --> PlaceHolder for Buildindex
    }
}
