using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    private VideoPlayer video;
    
    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();

        video.loopPointReached += LoadMainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu(video);
        }
    }

    private void LoadMainMenu(VideoPlayer vp)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
