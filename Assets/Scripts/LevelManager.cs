using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    private int enemiesKilled;
    [SerializeField] private int enemiesToKill;
    [SerializeField] private string nextScene; 

    public void enemyKilled()
    {
        enemiesKilled++;
        if(enemiesKilled == enemiesToKill) 
        {
            GameManager.getGameManager().ChangeScene(nextScene);
        }
    }

    
}
