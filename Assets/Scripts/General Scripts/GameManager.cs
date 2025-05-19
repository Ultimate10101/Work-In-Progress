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
        get { return gameWin; }
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
        set { isStoryPanelRunning = value; }

    }

    [SerializeField] private GameObject deathScreen;
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


    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;

        gamePaused = false;

        deathScreen.SetActive(false);
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
