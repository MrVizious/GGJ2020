using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Get an instance of the GameManager
    /// </summary>
    /// <returns>GameManager instance</returns>
    public static GameManager getGameManager()
    {
        return instance;
    }

    /// <summary>
    /// Loads the scene given as a parameter
    /// </summary>
    /// <param name="sceneName">String with the name of the scene to load</param>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    /// <summary>
    /// Loads the scene given as a parameter
    /// </summary>
    /// <param name="sceneIndex">Int with the index of the scene to load</param>
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
    public void NextScene(float waitTime)
    {
        Invoke("NextScene", waitTime);
    }
}