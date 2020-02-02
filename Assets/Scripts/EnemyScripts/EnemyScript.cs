using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;
    [SerializeField]
    private float speed, rotationSpeed, spawnDistance, freezeTime;
    private Rigidbody2D rb;
    [SerializeField]
    Transform target;
    [SerializeField]
    private ObjectPool bulletPool;

    private bool frozen;

    LayerMask spawnMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        spawnMask = 1 << LayerMask.GetMask("Wall") << LayerMask.GetMask("Enemy");
        frozen = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!frozen) Movement();
    }

    private void Movement()
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
            InstantiateBullets();
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

    public void InstantiateBullets()
    {
        for (int i = 0; i < maxHealth;)
        {
            Vector2 randomAngle = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * spawnDistance;
            if (!IsObstacleInDirection(randomAngle))
            {
                GameObject bullet = bulletPool.InstantiateFromPool(transform.position + (Vector3)randomAngle * spawnDistance, Quaternion.Euler(0, 0, Random.Range(0f, 359f)));
                bullet.GetComponent<BulletScript>().setBulletPool(bulletPool);
                bullet.GetComponent<BulletScript>().setTarget(target);
                i++;
            }
        }
    }

    private bool IsObstacleInDirection(Vector2 randomAngle)
    {
        return Physics2D.Raycast(transform.position, randomAngle, spawnDistance, spawnMask);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerScript>().Hurt();
        }
    }

    public void Freeze(float time)
    {
        frozen = true;
        StartCoroutine("FrozenCountdown");
    }

    IEnumerator FrozenCountdown()
    {
        yield return new WaitForSeconds(freezeTime);
        frozen = false;
    }
}
