using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Animator[] mainMenuButtons;

    public void Start()
    {
        foreach(Animator anim in mainMenuButtons)
        {
            anim.keepAnimatorStateOnDisable = true;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("2D_GameIntro", LoadSceneMode.Single); 
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }

}
