using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Transform t;
    private float rightJoystickX, rightJoystickY, leftJoystickX, leftJoystickY;
    private bool shootButtonDown;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        shootButtonDown = false;
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
        Vector2 goal = new Vector2(t.position.x + leftJoystickX * Time.deltaTime * speed, t.position.y + leftJoystickY * Time.deltaTime * speed);
        t.position = Vector2.MoveTowards(t.position, goal, speed);

        if (rightJoystickX != 0f || rightJoystickY != 0)
        {
            float rot_z = Mathf.Atan2(rightJoystickX, rightJoystickY) * Mathf.Rad2Deg;
            t.rotation = Quaternion.Euler(0f, 0f, rot_z - 180);
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
        if (shootButtonDown)
        {
            if (t.localScale.x > 1f) t.localScale -= new Vector3(1, 1, 0);
        }
    }

}
