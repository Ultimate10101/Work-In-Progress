using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerRef;

    private bool gameOver;

    public bool GameOver
    {
      get { return gameOver; } 
      set { gameOver = value; }
    }


    private bool gameWin;

    public bool GameWin
    {
        get { return GameWin; }
        set { gameWin = value; }

    }

    private bool gamePaused;

    public bool GamePaused
    {
        get { return gamePaused; }
        set { gamePaused = value; }   
    }

    private bool isStoryPanelRunning;

    public bool IsStoryPanelRunning
    {
        get { return isStoryPanelRunning; }

    }

    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject storyPanel;
    [SerializeField] private GameObject winScreen;

    private void Awake()
    {
        if (gameManagerRef == null)
        {
            gameManagerRef = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public enum PlayerInSection
    {
        PLAYER_IN_COMBAT,
        PLAYER_EXPLORING,

    }

    private PlayerInSection playerInSection;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;

        gamePaused = false;

        deathScreen.SetActive(false);

        playerInSection = PlayerInSection.PLAYER_EXPLORING;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            deathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        if(gameWin)
        {

            winScreen.SetActive(true);

            StartCoroutine(Delay());
        }
    }


    void PlayerInSectionTransition()
    {
        switch (playerInSection)
        {
            case PlayerInSection.PLAYER_IN_COMBAT:
                break;
            case PlayerInSection.PLAYER_EXPLORING:
                break;
        }


        // Changes Player States and Set variables
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3.0f);

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }


}
