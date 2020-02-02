using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    [SerializeField]
    private float chargePercentage;
    [SerializeField]
    private float healTime;
    private bool isHealing;
    // Start is called before the first frame update
    void Start()
    {
        chargePercentage = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (chargePercentage >= 1f)
        {
            chargePercentage = 0f;
            Debug.Log("Passive Heal!");
            StartCoroutine("Timer");
            StartCoroutine("Heal");
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

    public void Heal(float time)
    {
        healTime = time;
        isHealing = true;
        StartCoroutine("FrozenCountdown");
    }

    IEnumerator FrozenCountdown()
    {
        GetComponent<PlayerScript>().Heal();
        yield return new WaitForSeconds(healTime);
        isHealing = false;
    }


}
