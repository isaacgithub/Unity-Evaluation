using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMachine : MonoBehaviour {

	public bool buttonOn;
	public bool buttonOff;
	private Vector3 startSize;
	public SlotMachine machine;
	public static bool startIA = false;
	public bool buttonActive = false;
	public ChangeMachine bChange;
	// Use this for initialization
	void Start () {
		buttonOn = false;
		buttonOff = true;
		startIA = false;
		startSize = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (SlotMachine.turn == 2 && startIA && buttonActive && machine.playerNumber == 2) {
			startIA = false;
			/*int monsterActives = 0;
			for (int i = 0; i < 3; i++) {
				if (GameObject.Find ("Player02").transform.GetChild (i).GetComponent<Monster> ().active) {
					monsterActives++;
				}
			}
			if (monsterActives >= 2) {
				Invoke ("IAPressButtonChange", 1);
			} else {
				Invoke ("IAPressButton", 1);
			}*/
			Invoke ("IAPressButton", 1);
		}
	}

	void IAPressButton(){
		OnMouseDown ();
		Invoke ("IAStopButton", 1);
	}

	void IAStopButton(){
		OnMouseDown ();
	}

	void IAPressButtonChange(){
		startIA = true;
		bChange.OnMouseDown ();
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
