using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool gameIsPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (gameIsPaused)
            {
                // pausar juego y enseñar menu
                PauseGame();
            }
            else
            {
                // despausar y esconder menu
                ResumeGame();
            }
        } else if (Input.GetButtonDown("Circle"))
        {
            GameManager.getGameManager().ChangeScene("Menu");
        }
    }

    void PauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
