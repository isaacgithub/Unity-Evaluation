using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoPopUp : MonoBehaviour {

	public bool up = true;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 1);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(up)
			transform.Translate (0, 0.1f, 0);
	}
}
