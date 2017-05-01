using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMachine : MonoBehaviour {
	
	public GameObject[] machines;
	private int machineNow = 0;
	public Vector3 positionOff;
	public Vector3 positionOn;
	private Vector3 startSize;
	public int playerNumber;

	public float maxUp;
	public float minDown;
	// Use this for initialization
	void Start () {
		startSize = transform.localScale;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!SlotMachine.endGame) {
			if (SlotMachine.turn == playerNumber)
				upButton ();
			else { 
				downButton ();
			}
		}
	}

	public void OnMouseDown(){
		if (SlotMachine.startGame) {
			if (ButtonMachine.contButtonClick == 0) {
				if (!SlotMachine.endGame) {
					transform.localScale = new Vector3 (transform.localScale.x / 1.3f, transform.localScale.y / 1.3f, transform.localScale.z / 1.3f);
					Invoke ("defaultSize", 0.1f);
					machines [machineNow].transform.position = positionOff;
					machines [machineNow].transform.FindChild ("ButtonMachine").GetComponent<ButtonMachine> ().buttonActive = false;
					machineNow++;
					if (machineNow > machines.Length - 1)
						machineNow = 0;
					machines [machineNow].transform.position = positionOn;
					machines [machineNow].transform.FindChild ("ButtonMachine").GetComponent<ButtonMachine> ().buttonActive = true;
				}
			}
		}
	}

	void defaultSize(){
		transform.localScale = startSize;
	}

	void upButton(){
		if(transform.position.y < maxUp)
			transform.Translate (0, 0.1f, 0);
	}

	void downButton(){
		if(transform.position.y > minDown)
			transform.Translate (0, -0.1f, 0);
	}
}
