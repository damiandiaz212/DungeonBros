using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

    Enemy parent;
    
	void Start () {
        parent = GetComponentInParent<Enemy>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            parent.pursue = true;
        }
    }
}
