using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_2D : MonoBehaviour
{
    public static GameManager_2D gameManager2DRef;

    [SerializeField] private StoryPanel storyPanel;

    public bool isStoryPanelRunning;

    public bool IsStoryPanelRunning
    {

        get { return isStoryPanelRunning; }
    }

    private void Awake()
    {
        if (gameManager2DRef == null)
        {
            gameManager2DRef = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (storyPanel.IsActive)
        {
            isStoryPanelRunning = true;
        }
        else
        {
            isStoryPanelRunning = false;
        }
    }
}
