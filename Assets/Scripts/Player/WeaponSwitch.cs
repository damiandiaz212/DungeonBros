using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

    private int weapon;
    PlayerShoot script;

	// Use this for initialization
	void Start () {
		script = GetComponent<PlayerShoot>();
        weapon = script.weaponNum;
    }
	
	// Update is called once per frame
	void Update () {

        float mouse = Input.GetAxis("Mouse ScrollWheel");

        if(mouse > 0){

            if(weapon >= script.totalNum-1){
                weapon = 1;
            }else{
                weapon+=1;
            }

            script.SelectWeapon(weapon);
        }
        else if(mouse < 0){
           
            if (weapon <= 1)
            {
                weapon = script.totalNum-1;
            }
            else
            {
                weapon-=1;
            }

            script.SelectWeapon(weapon);
        }
	}
}
