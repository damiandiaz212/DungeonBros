using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        float size = Random.Range(0.4f, 0.7f);
        transform.localScale = new Vector3(size, size, size);
        
    }

    void Update()
    {
        Destroy(gameObject, 0.01f);
    }
	
	
}
