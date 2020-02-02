using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidEnemyScript : MonoBehaviour
{
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("This cell died!", this);
    }

}
