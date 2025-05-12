using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;

    void Update()
    {
        if (!GameManager.gameManagerRef.GameOver)
        {
            PauseMenu();
        }
        
    }

    void PauseMenu()
    {
        Debug.Log("I'm running");

        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf && !settingsMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0); // --> PlaceHolder for Buildindex
    }
}
