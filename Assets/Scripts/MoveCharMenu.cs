using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharMenu : MonoBehaviour {

	public float speedX;
	public float maxX;
	public float minX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > maxX) {
			transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
			speedX = (-1) * speedX;
		}
		if (transform.position.x < minX) {
			speedX = (-1) * speedX;
			transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
		}
		transform.Translate (speedX * Time.deltaTime, 0, 0);
	}
}
