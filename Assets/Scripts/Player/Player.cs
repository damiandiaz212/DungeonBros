using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public class PlayerStats
    {
        public int Health = 100;
    }

    public PlayerStats stats = new PlayerStats();
    public float speed;

    private Vector2 inputVector;
    private Vector2 movementVector;
    private Vector2 curPos;

    public GameObject weapon;
    private SpriteRenderer weaponSpr;
    private SpriteRenderer playerSpr;

    Rigidbody2D rb;
    Animator anim;

    bool isMoving = false;

    private void Awake () {
        rb = GetComponent<Rigidbody2D>();
        weaponSpr = weapon.GetComponentInChildren<SpriteRenderer>();
        playerSpr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}

    public void updateWeapon(){
        weaponSpr = weapon.GetComponentInChildren<SpriteRenderer>();
    }

	private void Update () {

        float xMov = Input.GetAxis("Horizontal");
        float yMov = Input.GetAxis("Vertical");

        movementVector = new Vector2(xMov, yMov);
        movementVector = Vector2.ClampMagnitude(movementVector, 1);
        movementVector = movementVector * speed * Time.deltaTime;

        curPos = new Vector2(transform.position.x, transform.position.y);

        faceMouse();

        if (xMov != 0 || yMov != 0)
            isMoving = true;
        else
            isMoving = false;

        if (isMoving)
            anim.SetBool("running", true);
        else
            anim.SetBool("running", false);

    }

    /// <summary>
    /// Rigidbody calls here.
    /// </summary>
    private void FixedUpdate()
    {
        if (isMoving)
            rb.MovePosition(curPos + movementVector);
    }

    /// <summary>
    /// Controls the direction of player sprite and weapon.
    /// </summary>
    private void faceMouse()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (angle > 90 || angle < -90)
        {
            weaponSpr.flipY = true;
            playerSpr.flipX = true;
        }
        else
        {
            weaponSpr.flipY = false;
            playerSpr.flipX = false;
        }
                        
    }

    /// <summary>
    ///  Pass damage value to player here.
    /// </summary>
    /// <param name="damage">int amount to reduce from "Health"</param>
    public void takeDamage(int damage)
    {
        stats.Health -= damage;

        if(stats.Health <= 0)
        {
            //TODO - Create death function
            Debug.LogError(this.name + " is DEAD.");
           
        }
    }

}
