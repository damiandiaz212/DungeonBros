using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float damage;
    public int liveTime;
    public int speed;

    public Transform hitPrefab;

    [HideInInspector]
    public Rigidbody2D rb;

	private void Update()
	{
        
        Destroy(gameObject, liveTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
        
        if(other.tag == "Enemy" || other.tag == "Destructable"){
            other.SendMessage("takeDamage", damage);

        }

        Transform particle = Instantiate(hitPrefab, transform.position, transform.rotation) as Transform;
        Destroy(particle.gameObject, 1);
        Destroy(gameObject);
	}
}
