using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float speed, thrust;
    private bool leaving;

    private Rigidbody2D rb;
    private Transform t;
    public Transform target;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (leaving)
        {
            if (rb.velocity.magnitude <= 0.1f) leaving = false;
        }
        else
        {
            RotateTowardsTarget();
            rb.MovePosition(Vector2.Lerp(t.position, t.position + t.up, speed * Time.deltaTime));
        }


    }


    private void OnEnable()
    {
        leaving = true;
        rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
    }

    private void OnDisable()
    {
        leaving = false;
    }

    private void RotateTowardsTarget()
    {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }


}
