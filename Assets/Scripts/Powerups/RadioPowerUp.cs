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
    [SerializeField]
    private float waveScaleMultiplier;
    private bool isIncreasing;
    void Start()
    {
        chargePercentage = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire2") && chargePercentage >= 1f)
        if (Input.GetKeyDown(KeyCode.R) && chargePercentage >= 1f)
        {
            chargePercentage = 0f;
            Debug.Log("Radio Activated!");
            ActivateRadioWave();
            StartCoroutine("Timer");
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

    void ActivateRadioWave()
    {
        isIncreasing = true;
        StartCoroutine("TimerScale");

        while (isIncreasing)
        {
            radioWave.transform.localScale = radioWave.transform.localScale * waveScaleMultiplier;
        }
    }

    IEnumerator TimerScale()
    {
        yield return new WaitForSeconds(3f);
        isIncreasing = false;
    }
}
