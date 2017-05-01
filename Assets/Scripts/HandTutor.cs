using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTutor : MonoBehaviour {

	public float speed;
	public float k = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		k += 0.1f;
		transform.Translate(0, Mathf.Cos (k)*speed, 0);
	}
}
