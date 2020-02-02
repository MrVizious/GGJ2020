using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    private void Update()
    {
        if (Input.GetButtonDown("Cross"))
        {
            SceneManager.LoadScene("1Petri");
        }
        if (Input.GetButtonDown("Circle"))
        {
            Application.Quit();
        }
    } 
}
