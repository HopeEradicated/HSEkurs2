using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenuObject;
    public GameObject videoPlayer;

    public static bool GameIsPaused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                GResume();
            }
            else
            {
                GPause();
            }
        }
    }

    public void GResume()
    {
        PauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
        //AudioListener.pause = false;
        videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
        GameIsPaused = false;
    }

    public void GPause()
    {
        PauseMenuObject.SetActive(true);
        Time.timeScale = 0f;
        //AudioListener.pause = true;
        videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        GameIsPaused = true;
    }
}
