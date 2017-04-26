using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackPot : MonoBehaviour {

	public float speed;
	public float maxY;
	public float minY;
	public float center;
	public bool stopMachine;
	public int result;

	// Use this for initialization
	void Start () {
		stopMachine = true;
		result = -1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!stopMachine) 
		{
			transform.Translate (0, speed * Time.deltaTime, 0);
			if (transform.localPosition.y > maxY)
				transform.localPosition = new Vector3 (transform.localPosition.x, minY, transform.localPosition.z);
			showAndHide();
		}
	}

	void showAndHide(){
		if (transform.localPosition.y < center - 0.6f)
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		else if (transform.localPosition.y > center + 0.6f)
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		else
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	}

	bool stopTest(){
		if (result == int.Parse(name)) {
			while (transform.position.y < -0.1f || transform.position.y > 0.1f) {
			}
			result = -1;
			return true;
		}
		return false;
	}


}
