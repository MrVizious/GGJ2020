using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetButtonDown("Cross"))
        {
            SceneManager.LoadScene("Menu");
             print ("it just works");
        }
       
       
    }
}
