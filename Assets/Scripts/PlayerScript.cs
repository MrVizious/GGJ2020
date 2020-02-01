using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    private float rightJoystickX, rightJoystickY, leftJoystickX, leftJoystickY;
    private bool shootButtonDown;

    public Transform bulletPrefab;

    [SerializeField]
    private int maxSize, currentSize;

    LayerMask shootWallMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootButtonDown = false;
        currentSize = maxSize;
        shootWallMask = LayerMask.GetMask("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();

        Shoot();

    }

    private void FixedUpdate()
    {
        Movement();
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
        if (shootButtonDown && currentSize > 1f && !IsWallAhead())
        {
            Shrink();
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.up * 0.25f * currentSize, transform.rotation).gameObject;
            bullet.GetComponent<BulletScript>().setTarget(transform);
            bullet.GetComponent<BulletScript>().Shoot();

        }
    }

    private bool IsWallAhead()
    {
        return Physics2D.Raycast(transform.position, transform.up, 0.3f * currentSize, shootWallMask);
    }

    public bool Heal()
    {
        if (currentSize < maxSize)
        {
            Grow();
            return true;
        }
        return false;
    }

    public void Shrink()
    {
        currentSize--;
        FixSize();
    }

    public void Grow()
    {
        currentSize++;
        FixSize();
    }

    private void FixSize()
    {
        transform.localScale = new Vector3(currentSize, currentSize, transform.localScale.z);
    }

}
