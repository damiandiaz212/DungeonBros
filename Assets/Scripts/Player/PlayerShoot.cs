using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    
    public Transform muzzleFlash;
    public Transform projectileSpawnPoint;
    public Transform hitPrefab;
    public LayerMask notToHit;

    private Transform bullet;
    public Player player;

    SpriteRenderer ren;

    //Mechanic properties
    private float timeBetweenShotsCounter;
    private bool canShoot;

    //Camera effect properties
    float shakeAmt = 0.03f;
    CameraShake cameraShake;

    //Weapon specific properties
    public int weaponNum;
    public int totalNum;

    private Transform weaponObj;
    private float damage;
    private float distance;
    private float timeBetweenShots;
    private bool shotgun;
    private bool automatic;
    private float spread;
    private Animator anim;

    private void Awake()
    {
        SelectWeapon(weaponNum);
    }
    void Start()
    {
        canShoot = true;
        timeBetweenShotsCounter = timeBetweenShots;
        cameraShake = GameMaster.gm.GetComponent<CameraShake>();

        if (cameraShake == null)
            Debug.LogError("No camera shake script found in gamemaster");

        int i = 0;

        foreach (Transform weapon in transform)
        {
            i++;
            totalNum++;
        }

    }

    public void SelectWeapon(int num)
    {

        weaponNum = num;

        int i = 0;

        foreach (Transform weapon in transform){
            if (i == weaponNum)
            {
                weapon.gameObject.SetActive(true);
                weaponObj = weapon;
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }

        if (weaponObj != null)
        {

            Weapon weapCom = weaponObj.GetComponent<Weapon>();

            damage = weapCom.damage;
            timeBetweenShots = weapCom.timeBetweenShots;
            shakeAmt = weapCom.camShake;
            automatic = weapCom.automatic;
            bullet = weapCom.bullet;
            anim = weaponObj.GetComponent<Animator>();

        }else{
            Debug.Log("Weapon select system failed.");
        }

        player.updateWeapon();

    }

    void Update () {

        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(projectileSpawnPoint.position.x, projectileSpawnPoint.position.y);
        float mouseDist = Vector2.Distance(transform.position, mousePosition);
        float minDist = 0.7f; //NOTE: anything less than 0.7 will break core mechanic

        // Automatic Fire 
        if (!automatic)
        {
            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                if (mouseDist > minDist)
                {
                    
                    NewShoot();

                    canShoot = false;

                    if (anim != null)
                        anim.SetTrigger("fire");
                }
            }
        }

        // Semi-Automatic Fire
        else
        {
            if (Input.GetMouseButton(0) && canShoot)
            {
                if (mouseDist > minDist)
                {
                    
                    NewShoot();

                    canShoot = false;

                    if (anim != null)
                        anim.SetTrigger("fire");
                }
            }
        }

        if (!canShoot)
        {
            timeBetweenShotsCounter -= Time.deltaTime;
            if (timeBetweenShotsCounter <= 0)
            {
                canShoot = true;
                timeBetweenShotsCounter = timeBetweenShots;
            }
        }

       
    }

    void NewShoot()
    {

        Transform i = Instantiate(bullet, projectileSpawnPoint.position, transform.rotation) as Transform;
        // TODO: Possible effect multiplier?

        NewEffect();

    }

    void NewEffect()
    {
        Transform muz = Instantiate(muzzleFlash, projectileSpawnPoint.position, weaponObj.transform.rotation) as Transform;
        Destroy(muz.gameObject, 0.5f);

        cameraShake.Shake(shakeAmt, 0.1f);
    }

}
