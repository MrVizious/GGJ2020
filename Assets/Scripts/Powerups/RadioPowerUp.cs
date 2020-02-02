using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPowerUp : MonoBehaviour
{
    [SerializeField]
    private float chargePercentage;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject radioWave;
    private bool isIncreasing;
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
            Instantiate(radioWave, transform.position, transform.rotation);
        }
    }

    IEnumerator Timer()
    {
        float initTime = Time.time;

        while (chargePercentage < 1f)
        {
            chargePercentage = Time.time - initTime;
            yield return 0;
        }
        chargePercentage = 1f;
        yield return null;
    }
}
