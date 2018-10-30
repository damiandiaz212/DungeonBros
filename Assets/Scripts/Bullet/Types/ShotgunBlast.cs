using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBlast : MonoBehaviour {

    public Transform bullet;
    public int numberOfShots;
    public float damage;
    public int spread;

	void Start () {

       
        for (int i = 0; i < numberOfShots; i++)
        {
            
            Transform tempBullet = (Transform)Instantiate(bullet, transform.position, transform.rotation) as Transform;
            Bullet bScript = tempBullet.GetComponent<Bullet>();

            bScript.damage = damage;

            tempBullet.transform.Rotate(0, 0, Random.Range(-spread, spread));

            Destroy(gameObject, 1);



        }

	}
	
	
}
