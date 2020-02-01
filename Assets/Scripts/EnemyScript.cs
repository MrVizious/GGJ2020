using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;
    [SerializeField]
    private float speed, rotationSpeed;
    private Rigidbody2D rb;
    [SerializeField]
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateTowardsTarget();
        rb.MovePosition(Vector2.Lerp(transform.position, transform.position + transform.up, speed * Time.deltaTime));
    }

    private void RotateTowardsTarget()
    {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
    }

    public bool Heal()
    {
        if (currentHealth < maxHealth)
        {
            Grow();
            return true;
        }
        return false;
    }

    public bool Hurt()
    {
        currentHealth--;
        if (currentHealth > 0f)
        {
            Debug.Log("Current life is: " + currentHealth);
        }
        else if (currentHealth == 0f)
        {
            Debug.Log("TODO: Spawn little bullets to come back");
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

    public void Shrink()
    {
        currentHealth--;
        FixSize();
    }

    public bool Grow()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            Shrink();
            return true;
        }
        return false;
    }

    private void FixSize()
    {
        transform.localScale = new Vector3(currentHealth, currentHealth, transform.localScale.z);
    }
}
