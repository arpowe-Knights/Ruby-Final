using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    private float lifeTime = 10f;
    //RubyController ruby;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void Update()
    {

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);

        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();

        if (e != null)
        {
            e.Fix();
            RobotCounter.Instance.FixRobot();
        }

        Destroy(gameObject);

        //if (e || a != null)
        // {
        //   ruby.robotsCount += 1;
        // }
    }

}
