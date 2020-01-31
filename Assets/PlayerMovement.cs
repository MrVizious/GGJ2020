using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Transform t;
    private float rightJoystickX, rightJoystickY, leftJoystickX, leftJoystickY;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();


        if (rightJoystickX != 0f || rightJoystickY != 0)
        {
            float rot_z = Mathf.Atan2(rightJoystickX, rightJoystickY) * Mathf.Rad2Deg;
            t.rotation = Quaternion.Euler(0f, 0f, rot_z - 180);
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 goal = new Vector2(t.position.x + leftJoystickX * Time.deltaTime * speed, t.position.y + leftJoystickY * Time.deltaTime * speed);
        t.position = Vector2.MoveTowards(t.position, goal, speed);
    }

    private void GetInputs()
    {
        leftJoystickX = Input.GetAxis("Horizontal");
        leftJoystickY = Input.GetAxis("Vertical");
        rightJoystickX = Input.GetAxis("Mouse X");
        rightJoystickY = Input.GetAxis("Mouse Y");
    }
}
