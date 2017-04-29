using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAControl : MonoBehaviour {

	public static bool startIA = false;
	public ChangeMachine bChange;
	public ButtonMachine buttonSummon;
	public ButtonMachine buttonSummonAtk;
	public GameObject player2;
	private int currentMachine = 1;

	// Use this for initialization
	void Start () {
		startIA = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (SlotMachine.turn == 2 && startIA) {
			startIA = false;
			int contMonster = 0;
			for (int i = 0; i < 3; i++) {
				if (player2.transform.GetChild (i).GetComponent<Monster> ().active)
					contMonster++;
			}
			if (contMonster >= 2 && currentMachine == 1) {
				Invoke ("IAPressButtonChange", 1);
			} else if (contMonster < 2 && currentMachine == 2) {
				Invoke ("IAPressButtonChange", 1);
			} else {
				Invoke ("IAPressButton", 1);
			}

		}
	}

	void IAPressButton(){
		if(currentMachine == 1)
			buttonSummon.OnMouseDown ();
		else if(currentMachine == 2)
			buttonSummonAtk.OnMouseDown ();
		Invoke ("IAStopButton", 1);
	}

	void IAStopButton(){
		if(currentMachine == 1)
			buttonSummon.OnMouseDown ();
		else if(currentMachine == 2)
			buttonSummonAtk.OnMouseDown ();
	}

	void IAPressButtonChange(){
		if (currentMachine == 1)
			currentMachine = 2;
		else
			currentMachine = 1;
		startIA = true;
		bChange.OnMouseDown ();
	}
}
