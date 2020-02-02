using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyScript : EnemyScript
{
    [SerializeField]
    private int currentHealth, maxHealth;
    [SerializeField]
    private float speed, rotationSpeed, freezeTime;
    private Rigidbody2D rb;
    private Transform target;
    private ObjectPool bulletPool;

    private bool frozen;

    LayerMask spawnMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        spawnMask = ~((1 << LayerMask.GetMask("Wall")) | (1 << LayerMask.GetMask("Enemy")));
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

    public override bool Hurt()
    {
        currentHealth--;
        if (currentHealth > 0f)
        {
            Debug.Log("Current life is: " + currentHealth);
        }
        else if (currentHealth == 0f)
        {
            GetComponent<Collider2D>().enabled = false;
            InstantiateBullets();
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

    public void InstantiateBullets()
    {
        GetComponent<Collider2D>().enabled = false;
        LayerMask customMask = (1 << LayerMask.NameToLayer("Wall")) | (1 << LayerMask.GetMask("Bullet")) | (1 << LayerMask.GetMask("Enemy")) | (1 << LayerMask.GetMask("Player"));
        for (int i = 0; i < maxHealth;)
        {
            bool appeared = false;
            while (!appeared)
            {
                float bulletsDistance = Random.Range(0.5f, 1.5f);
                Vector2 randomAngle = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * bulletsDistance;
                if (!IsObstacleInDirection(randomAngle, bulletsDistance, customMask))
                {
                    appeared = true;
                    GameObject bullet = bulletPool.InstantiateFromPool(transform.position + (Vector3)randomAngle * bulletsDistance, Quaternion.Euler(0, 0, Random.Range(0f, 359f)));
                    bullet.GetComponent<BulletScript>().setBulletPool(bulletPool);
                    bullet.GetComponent<BulletScript>().setTarget(target);
                    i++;
                }
                else Debug.Log("Obstacle!");
            }
        }
        GetComponent<Collider2D>().enabled = true;
    }

    private bool IsObstacleInDirection(Vector2 angle, float distance, LayerMask mask)
    {
        Debug.DrawRay(transform.position, angle, Color.green, distance);
        return Physics2D.Raycast(transform.position, angle, distance, mask);
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
        freezeTime = time;
        frozen = true;
        StartCoroutine("FrozenCountdown");
    }

    IEnumerator FrozenCountdown()
    {
        yield return new WaitForSeconds(freezeTime);
        frozen = false;
    }

    public override void setTarget(Transform t) { target = t; }
    public override void setBulletPool(ObjectPool o) { bulletPool = o; }
}
