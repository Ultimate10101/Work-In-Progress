using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class StoryPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] storyText;

    private bool isFinished;

    private bool isActive;

    public bool IsActive
    {
        get { return isActive; }
    }

    void Start()
    {
        isFinished = false;

        isActive = true;

        if (GameManager.gameManagerRef != null)
        {
            GameManager.gameManagerRef.IsStoryPanelRunning = true;
        }

        if (gameObject.activeSelf)
        {
            StartCoroutine(ActivateTextEffect());
        }     
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished == true || Input.GetKeyDown(KeyCode.Escape))
        {

            gameObject.SetActive(false);

            isFinished = false; // --> For the sake of not having the statments inside run again

            isActive = false;

            if (GameManager.gameManagerRef != null)
            {
                GameManager.gameManagerRef.IsStoryPanelRunning = false;
            }
        }
    }


    private IEnumerator ActivateTextEffect()
    {
        foreach (TextMeshProUGUI text in storyText)
        {

            yield return StartCoroutine(FadeTextIn(text));

            yield return new WaitForSeconds(2.0f);
        }

        yield return new WaitForSeconds(4.0f);

        isFinished = true;
    }


    private IEnumerator FadeTextIn(TextMeshProUGUI text)
    {
        Color color = text.color;

        while (text.color.a < 1)
        {
            color.a += .1f;
            text.color = color;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
