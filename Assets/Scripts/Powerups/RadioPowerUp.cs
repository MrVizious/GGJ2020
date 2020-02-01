using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPowerUp : MonoBehaviour
{
    [SerializeField]
    private float chargePercentage;
    // Start is called before the first frame update
    void Start()
    {
        chargePercentage = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && chargePercentage >= 1f)
        {
            chargePercentage = 0f;
            Debug.Log("Radio Activated!");
            StartCoroutine("Timer");
        }
    }

    IEnumerator Timer()
    {
        float initTime = Time.time;

        while (chargePercentage < 1f)
        {
            chargePercentage = Time.time - initTime;
            Debug.Log(chargePercentage);
        }
        chargePercentage = 1f;
        yield return null;
    }
}
