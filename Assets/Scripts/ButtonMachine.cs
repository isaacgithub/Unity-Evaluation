using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMachine : MonoBehaviour {

	public bool buttonOn;
	public bool buttonOff;
	private Vector3 startSize;
	public SlotMachine machine;
	public bool buttonActive = false;
	public static int contButtonClick = 0;
	public AudioSource audio;

	// Use this for initialization
	void Start () {
		buttonOn = false;
		buttonOff = true;
		contButtonClick = 0;
		audio = GameObject.Find ("AudioMachine").GetComponent<AudioSource> ();
		startSize = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (IAControl.startIA)
			contButtonClick = 0;
	}

	public void OnMouseDown(){
		if (SlotMachine.startGame) {
			if (contButtonClick < 2) {
				audio.Play ();
				contButtonClick++;
				SlotMachine.machineRun = true;
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
		}
	}

	void defaultSize(){
		transform.localScale = startSize;
	}
}
