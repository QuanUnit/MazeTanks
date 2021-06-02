using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelFraction : Bullet
{
    private Rigidbody2D rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lifeTime = Random.Range(lifeTime - 0.2f, lifeTime + 0.2f);
        base.Start();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tank tank;
        if (collision.gameObject.TryGetComponent<Tank>(out tank) == true)
        {
            tank.TakeHit();
            DestroyBullet();
        }
        if (collision.gameObject.layer == 8) // wall
        {
            rigidbody.velocity /= 2;
            rigidbody.angularVelocity /= 2;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) // wall
        {
            rigidbody.velocity *= 2;
            rigidbody.angularVelocity *= 2;
        }
    }
}
