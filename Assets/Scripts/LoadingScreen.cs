using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public string levelToLoad;

    public GameObject loadscreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if  (Input.GetKeyDown(KeyCode.Mouse0))
        {
            loadscreen.SetActive(true);
            //SceneManager.Load(levelToLoad)
            StartCoroutine(LoadLevelAsync());

        }
    }

    private IEnumerator LoadLevelAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);

        while(!asyncLoad.isDone)
        {
            yield return null;
        }


    }
}
