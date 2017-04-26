using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackPot : MonoBehaviour {

	public float speed;
	public float maxY;
	public float minY;
	public float center;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (0, speed * Time.deltaTime, 0);
		if (transform.localPosition.y > maxY)
			transform.localPosition = new Vector3 (transform.localPosition.x, minY, transform.localPosition.z);

		if (transform.localPosition.y < center-0.6f)
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
		else if(transform.localPosition.y > center + 0.6f)
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
		else 
			gameObject.GetComponent<SpriteRenderer>().enabled = true;
	}
}
