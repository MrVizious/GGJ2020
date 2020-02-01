using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float speed, rotationSpeed, thrust;
    private bool leaving;

    private Rigidbody2D rb;
    private Transform t;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
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

    public void Shoot()
    {
        leaving = true;
        if (rb == null) rb = rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
    }

    private void RotateTowardsTarget()
    {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
    }

    public void setTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerScript>().Heal();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<EnemyScript>().Hurt();
            Destroy(this.gameObject);
        }
    }


}
