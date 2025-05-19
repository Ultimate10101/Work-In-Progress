using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    [SerializeField] private AudioClip pauseSound;

    void Update()
    {
        if (!GameManager.gameManagerRef.GameOver)
        {
            PauseMenu();
        }
        
    }

    private void PauseMenu()
    {
        Debug.Log("I'm running");

        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf && !settingsMenu.activeSelf && !GameManager.gameManagerRef.IsStoryPanelRunning && !GameManager.gameManagerRef.GameWin)
        {
            AudioManager.audioManagerRef.PlaySFX(pauseSound);

            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            GameManager.gameManagerRef.GamePaused = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ResumeGame()
    {

        AudioManager.audioManagerRef.PlaySFX(pauseSound);

        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        GameManager.gameManagerRef.GamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single); 
    }
}
