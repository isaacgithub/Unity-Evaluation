using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour {

	public GameObject slot01;
	public GameObject slot02;
	public GameObject slot03;
	public static Vector3 result;
	public bool machineStart = false;
	public bool stopOn = false;
	// Use this for initialization
	void Start () {
		machineStart = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.S) && !machineStart) {
			startMachine ();
			machineStart = true;
		}
		if (Input.GetKeyDown (KeyCode.P) && machineStart && !stopOn) {
			stopMachine ();
			stopOn = true;
		}
	}

	void startMachine(){
		for (int i = 0; i < 3; i++) {
			slot01.transform.GetChild (i).GetComponent<JackPot> ().stopMachine = false;
			slot02.transform.GetChild (i).GetComponent<JackPot> ().stopMachine = false;
			slot03.transform.GetChild (i).GetComponent<JackPot> ().stopMachine = false;
		}
	}

	void stopMachine(){
		result.x = Random.Range(0,3);
		result.y = Random.Range(0,3);
		result.z = Random.Range(0,3);
		Invoke ("stopSlot01", 0.5f);
		Invoke ("stopSlot02", 1);
		Invoke ("stopSlot03", 1.5f);
	}

	void stopSlot01(){
		stopSlot (slot01, (int)result.x);
	}

	void stopSlot02(){
		stopSlot (slot02, (int)result.y);
	}

	void stopSlot03(){
		stopSlot (slot03, (int)result.z);
		machineStart = false;
		stopOn = false;
	}

	void stopSlot(GameObject slot, int result){
		for (int i = 0; i < 3; i++) {
			slot.transform.GetChild (i).GetComponent<JackPot> ().stopMachine = true;
		}
		if (result == 0) {
			slot.transform.GetChild (0).transform.localPosition = new Vector3 (0, 0, 0);
			slot.transform.GetChild (1).transform.localPosition = new Vector3 (0, 1.23f, 0);
			slot.transform.GetChild (2).transform.localPosition = new Vector3 (0, -1.095f, 0);
			slot.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = true;
			slot.transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;
			slot.transform.GetChild (2).GetComponent<SpriteRenderer> ().enabled = false;
		}else if (result == 1) {
			slot.transform.GetChild (0).transform.localPosition = new Vector3 (0, 1.23f, 0);
			slot.transform.GetChild (1).transform.localPosition = new Vector3 (0, 0, 0);
			slot.transform.GetChild (2).transform.localPosition = new Vector3 (0, -1.095f, 0);
			slot.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = false;
			slot.transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = true;
			slot.transform.GetChild (2).GetComponent<SpriteRenderer> ().enabled = false;
		}if (result == 2) {
			slot.transform.GetChild (0).transform.localPosition = new Vector3 (0, -1.095f, 0);
			slot.transform.GetChild (1).transform.localPosition = new Vector3 (0, 1.23f, 0);
			slot.transform.GetChild (2).transform.localPosition = new Vector3 (0, 0, 0);
			slot.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = false;
			slot.transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;
			slot.transform.GetChild (2).GetComponent<SpriteRenderer> ().enabled = true;
		}

	}
}
