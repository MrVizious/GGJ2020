using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Grow");
        Debug.Log("Sprite created!");
    }

    IEnumerator Grow()
    {
        float startTime = Time.time;
        while (Time.time - startTime < 1f)
        {
            transform.localScale += new Vector3(0.025f, 0.025f, 0f);
            yield return 0;
        }
        Destroy(this.gameObject);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy")) other.gameObject.GetComponent<EnemyScript>().Freeze(2f);
    }

    private void Update()
    {
        Debug.Log("I am alive!", this);
    }
}
