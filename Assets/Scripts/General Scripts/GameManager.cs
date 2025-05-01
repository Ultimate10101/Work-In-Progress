using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerRef;

    private void Awake()
    {
        if(gameManagerRef == null)
        {
            gameManagerRef = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public enum GameStates
    {
        GAME_INTRO,
        GAMEPLAY,
        GAMEND
    }

    public enum PlayerInSection
    {
        PLAYER_IN_NO_SECTION,
        PLAYER_IN_COMBAT,
        PLAYER_EXPLORING,
        PLAYER_IN_PUZZEL
    }

    private GameStates gameState;
    private PlayerInSection playerInSection;


    private GameObject pauseMenu;
    private GameObject deathScreen;

    private P_PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameStates.GAME_INTRO;
        playerInSection = PlayerInSection.PLAYER_IN_NO_SECTION;

        pauseMenu = null;
        deathScreen = null;
    }

    // Update is called once per frame
    void Update()
    {
    
    }


    void PauseMenu()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    void StartGame()
    {
        SceneManager.LoadScene(0);  // --> PlaceHolder for Buildindex
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }

    void BackToMainMenu()
    {
        SceneManager.LoadScene(0); // --> PlaceHolder for Buildindex
    }

    void Settings()
    {
        // Look up how to do settings later
    }

    void DeathScreen()
    {
        deathScreen.SetActive(true);
    }

    void Respawn()
    {
        // Set Player Back to the beginning and refresh enemies on map, but not puzzels
    }
    

    void GameStatesTransition()
    {
        // Changes Game States and set variables
    }

    void PlayerInSectionTransition()
    {
        // Changes Player States and Set variables
    }


    void GameInIntro()
    {

    }

    void GameInGameplay()
    {

    }

    void GameInEnd()
    {

    }


}
