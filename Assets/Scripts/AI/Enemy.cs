using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // Public declerations
    public float speed;
    public float stoppingDistance;
    public Transform blood;
    public int bloodChance;
    public float health;

    // Private declerations
    private Transform target;
    private BoxCollider2D col;
    private Animator anim;
    private SpriteRenderer ren;

    public bool pursue;
    private bool isDead;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        ren = GetComponent<SpriteRenderer>();

        pursue = false;
        isDead = false;
    }

    void Update()
    {
        if (!isDead && pursue)
        {
            if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }

            if (transform.position.x > target.position.x)
                ren.flipX = true;
            else
                ren.flipX = false;
        }
            
    }

   

    public void takeDamage(float damage)
    {

        health -= damage;

        if (health <= 0)
            death();


        //Blood effect
        if(Random.Range(0, bloodChance) == 0)
            Instantiate(blood, transform.position, transform.rotation);
    }

    private void death(){
        isDead = true;
        col.enabled = false;
        anim.SetTrigger("dead");
        Destroy(gameObject, 2f);
    }
}
