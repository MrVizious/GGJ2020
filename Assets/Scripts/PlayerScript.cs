using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float speed, maxSize, minSize;
    private Rigidbody2D rb;
    private float rightJoystickX, rightJoystickY, leftJoystickX, leftJoystickY;
    [SerializeField]
    private bool shootButtonDown, invincible;

    [SerializeField]
    private ObjectPool bulletPool;

    [SerializeField]
    private int maxHealth, currentHealth;

    LayerMask shootWallMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootButtonDown = false;
        currentHealth = maxHealth;
        shootWallMask = LayerMask.GetMask("Wall");
        invincible = false;
        FixSize();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        CheckGameOver();
        Shoot();

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void CheckGameOver()
    {
        if (bulletPool.IsFull() && currentHealth <= 1f) Die();
    }

    private void Movement()
    {
        //Movement
        Vector2 goal = new Vector2(transform.position.x + leftJoystickX * Time.deltaTime * speed, transform.position.y + leftJoystickY * Time.deltaTime * speed);
        rb.MovePosition(new Vector2(transform.position.x + leftJoystickX * Time.deltaTime * speed, transform.position.y + leftJoystickY * Time.deltaTime * speed));

        //Rotation
        if (rightJoystickX != 0f || rightJoystickY != 0)
        {
            float rot_z = Mathf.Atan2(rightJoystickX, rightJoystickY) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 180);
        }
    }

    //Stores in variables
    private void GetInputs()
    {
        leftJoystickX = Input.GetAxis("Horizontal");
        leftJoystickY = Input.GetAxis("Vertical");
        rightJoystickX = Input.GetAxis("Mouse X");
        rightJoystickY = Input.GetAxis("Mouse Y");
        if (Input.GetButtonDown("Fire1")) shootButtonDown = true;
        else shootButtonDown = false;
    }

    private void Shoot()
    {
        if (shootButtonDown && currentHealth > 1f && !IsWallAhead())
        {
            Shrink();
            GameObject bullet = bulletPool.InstantiateFromPool(transform.position + transform.up * 0.35f * currentHealth, transform.rotation);
            bullet.GetComponent<BulletScript>().setTarget(transform);
            bullet.GetComponent<BulletScript>().setBulletPool(bulletPool);
            bullet.GetComponent<BulletScript>().Shoot();
        }
    }

    private bool IsWallAhead()
    {
        return Physics2D.Raycast(transform.position, transform.up, 0.3f * currentHealth, shootWallMask);
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

    public void Shrink()
    {
        currentHealth--;
        FixSize();
    }

    public void Grow()
    {
        currentHealth++;
        FixSize();
    }

    private void FixSize()
    {
        float normalizedValue = Mathf.InverseLerp(1, maxHealth, currentHealth);
        float result = Mathf.Lerp(minSize, maxSize, normalizedValue);
        transform.localScale = new Vector3(result, result, transform.localScale.z);
    }

    public void Hurt()
    {
        if (!invincible)
        {
            if (currentHealth <= 1f) Die();
            Debug.Log("You were hurt!");
            Shrink();
            StartCoroutine("InvincibleCountdown");
        }
    }

    private void Die()
    {
        Debug.Log("You died!");
        Destroy(this.gameObject);
    }

    IEnumerator InvincibleCountdown()
    {
        invincible = true;
        yield return new WaitForSeconds(1);
        invincible = false;

    }

    public void MultiplySpeed(float multiplier)
    {
        speed *= multiplier;
    }


}
