using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Transform t;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 goal = new Vector2(t.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed, t.position.y + Input.GetAxis("Vertical") * Time.deltaTime * speed);
        t.position = Vector2.MoveTowards(t.position, goal, speed);

        float rot_z = Mathf.Atan2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Mathf.Rad2Deg;
        t.rotation = Quaternion.Euler(0f, 0f, rot_z - 180);
    }
}
