using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject Jugar, Salirs;
    public bool ON, Mute;
   


    public void aPlay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void aExit()
    {
        Application.Quit();
    }   
}
