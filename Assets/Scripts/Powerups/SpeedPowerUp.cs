using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpeedPowerUp : MonoBehaviour
{
    [SerializeField]
    private float multiplier;
    // Update is called once per frame
    void Start()
    {
        GetComponent<PlayerScript>().MultiplySpeed(multiplier);
    }
}
