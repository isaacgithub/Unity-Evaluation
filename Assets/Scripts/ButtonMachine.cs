using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMachine : MonoBehaviour {

	public bool buttonOn;
	public bool buttonOff;
	private Vector3 startSize;
	public SlotMachine machine;
	// Use this for initialization
	void Start () {
		buttonOn = false;
		buttonOff = true;
		startSize = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		if (SlotMachine.turn == machine.playerNumber) {
			transform.localScale = new Vector3 (transform.localScale.x / 1.3f, transform.localScale.y / 1.3f, transform.localScale.z / 1.3f);
			buttonOn = !buttonOn;
			buttonOff = !buttonOff;
			if (!buttonOff)
				GetComponent<SpriteRenderer> ().color = Color.green;
			else
				GetComponent<SpriteRenderer> ().color = Color.white;
			Invoke ("defaultSize", 0.1f);
		}
	}

	void defaultSize(){
		transform.localScale = startSize;
	}
}
