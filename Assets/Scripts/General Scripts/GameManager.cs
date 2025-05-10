using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameStates.GAME_INTRO;
        playerInSection = PlayerInSection.PLAYER_IN_NO_SECTION;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    void GameStatesTransition()
    {
        switch (gameState)
        {
            case GameStates.GAME_INTRO:
                break;
            case GameStates.GAMEPLAY:
                break;
            case GameStates.GAMEND:
                break;
        }

        // Changes Game States and set variables
    }

    void PlayerInSectionTransition()
    {
        switch (playerInSection)
        {
            case PlayerInSection.PLAYER_IN_COMBAT:
                break;
            case PlayerInSection.PLAYER_EXPLORING:
                break;
            case PlayerInSection.PLAYER_IN_PUZZEL:
                break;
        }


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
