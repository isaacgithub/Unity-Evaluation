using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour {

	public GameObject slot01;
	public GameObject slot02;
	public GameObject slot03;
	public Vector3 result;
	public bool machineStart = false;
	public bool stopOn = false;

	public GameObject Player;
	public int playerNumber;
	public static int turn = 1;
	public ButtonMachine button;
	public TextMesh turnName;
	public bool firstTurn = true;

	public float maxUp;
	public float minDown;

	public float[] mapSlot;

	// Use this for initialization
	void Start () {
		turn = 1;
		machineStart = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (button.buttonOn && !machineStart) {
			startMachine ();
			machineStart = true;
		}
		if (button.buttonOff && machineStart && !stopOn) {
			stopMachine ();
			stopOn = true;
		}

		if (turn == playerNumber)
			upMachine ();
		else { 
			downMachine ();
			firstTurn = false;
		}
	}

	void upMachine(){
		if(transform.position.y < maxUp)
			transform.Translate (0, 0.1f, 0);
	}

	void downMachine(){
		if(transform.position.y > minDown)
			transform.Translate (0, -0.1f, 0);
	}

	void startMachine(){
		slot01.GetComponent<Slot> ().stopMachine = false;
		slot02.GetComponent<Slot> ().stopMachine = false;
		slot03.GetComponent<Slot> ().stopMachine = false;
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
		Material currentMaterial = slot01.GetComponent<Renderer>().material;
		slot01.GetComponent<Slot> ().stopMachine = true;
		currentMaterial.SetTextureOffset ("_MainTex", new Vector2 (0, mapSlot[(int)result.x]));
	}

	void stopSlot02(){
		Material currentMaterial = slot02.GetComponent<Renderer>().material;
		slot02.GetComponent<Slot> ().stopMachine = true;
		currentMaterial.SetTextureOffset ("_MainTex", new Vector2 (0, mapSlot[(int)result.y]));
	}

	void stopSlot03(){
		Material currentMaterial = slot03.GetComponent<Renderer>().material;
		slot03.GetComponent<Slot> ().stopMachine = true;
		currentMaterial.SetTextureOffset ("_MainTex", new Vector2 (0, mapSlot[(int)result.z]));

		machineStart = false;
		stopOn = false;

		Invoke ("battleStart", 0.2f);

		Player.transform.GetChild ((int)result.x).GetComponent<Monster> ().active = true;
		Player.transform.GetChild ((int)result.y).GetComponent<Monster> ().active = true;
		Player.transform.GetChild ((int)result.z).GetComponent<Monster> ().active = true;
	}

	private List<Monster> monsters;
	void battleStart(){
		Monster[] allMonsters = Resources.FindObjectsOfTypeAll<Monster>();
		monsters = new List<Monster> ();
		foreach(Monster m in allMonsters){
			if (m.active) {
				monsters.Add (m);
			}
		}
		if (!firstTurn) {
			foreach (Monster m in monsters) {
				m.battleFase = true;
			}
		}
		Invoke ("changeTurn", 1f);

	}

	void changeTurn(){
		turn++;
		if (turn > 2) {
			turn = 1;
		}
	}
}
