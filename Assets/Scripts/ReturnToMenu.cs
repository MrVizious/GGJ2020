using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown("joystick button 9"))
        {
            SceneManager.LoadScene("Menu");
             print ("it just works");
        }
       
       
    }
}
