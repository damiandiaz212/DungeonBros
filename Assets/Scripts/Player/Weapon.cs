using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float damage;
    public float distance;
    public float timeBetweenShots;
    public float camShake;
    public bool automatic;
    public Transform bullet;
    public Animator animator;
}
