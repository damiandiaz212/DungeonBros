using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

    private Animator anim;
    private BoxCollider2D col;
    private SpriteRenderer ren;
    public Sprite[] damageStates;
    public float health;
    private float[] damagePoints;
    private int checkpoint = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        ren = GetComponent<SpriteRenderer>();

        damagePoints = new float[damageStates.Length];

        float curPoint = health;

        for(int i = 0; i < damageStates.Length; i++)
        {
            curPoint  = (curPoint - (health / damageStates.Length)) + 10;

            if (curPoint >= 0)
                damagePoints[i] = curPoint;
            else
                damagePoints[i] = 0;
        }

    }

    private void Update()
    {
        if (health <= 0)
            Destroyed();
    }

    public void takeDamage(float damage)
    {
   
        health -= damage;

        if (checkpoint < damageStates.Length)
        {
            if (health <= damagePoints[checkpoint])
            {
                ren.sprite = damageStates[checkpoint];
                checkpoint++;
            }
        }
    }

    private void Destroyed()
    {
        anim.enabled = true;
        col.enabled = false;
    }

}
