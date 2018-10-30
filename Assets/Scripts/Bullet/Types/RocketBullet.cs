using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBullet : Bullet {

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }
}
