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
	public string identify;
	public static bool startGame = false;
	public GameObject Player;
	public int playerNumber;
	public static int turn = 1;
	public ButtonMachine button;
	public TextMesh turnName;
	public bool firstTurn = true;
	public static bool endGame = false;

	public float maxUp;
	public float minDown;

	public static bool machineRun = false;

	public float[] mapSlot;

	// Use this for initialization
	void Start () {
		turn = 1;
		endGame = false;
		machineRun = false;
		machineStart = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!endGame) {
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

		if (identify == "SlotSummon") {
			summon ();
		} else if (identify == "SlotAtk") {
			waitAtksX ();
		} else if (identify == "SlotDef") {
			defense ();
		}
	}

	void activeMonster(Monster m, int gainHp, int raiseL){
		if (m.active) {
			m.raiseHp (gainHp);
		}else {
			m.active = true;
			for(int i=0;i<raiseL;i++)
				m.raiseLevel ();
		}
	}

	void waitAtksX(){
		Player.GetComponent<Boss> ().attack ((int)result.x);
		Invoke ("waitAtksY", 0.5f);
	}
	void waitAtksY(){
		Player.GetComponent<Boss> ().attack ((int)result.y);
		Invoke ("waitAtksZ", 0.5f);
	}
	void waitAtksZ(){
		Player.GetComponent<Boss> ().attack ((int)result.z);
	}

	void summon(){
		Monster m1 = Player.transform.GetChild ((int)result.x).GetComponent<Monster> ();
		Monster m2 = Player.transform.GetChild ((int)result.y).GetComponent<Monster> ();
		Monster m3 = Player.transform.GetChild ((int)result.z).GetComponent<Monster> ();

		if (m1 != m2 && m1 != m3 && m2 != m3) { // All Different
			activeMonster (m1, 400, 0);
			activeMonster (m2, 400, 0);
			activeMonster (m3, 400, 0);
		}

		if (m1 == m2 && m2 == m3) { // All the same
			activeMonster (m1, 800, 2);
		}

		if (m1 == m2 && m2 != m3) { // m1 == m2 and m2 != m3
			activeMonster (m1, 600, 1);
			activeMonster (m3, 400, 0);
		}

		if (m1 != m2 && m2 == m3) { // m1 != m2 and m2 == m3
			activeMonster (m1, 400, 0);
			activeMonster (m2, 600, 1);
		}

		if (m1 != m2 && m1 == m3) { // m1 != m2 and m1 == m3
			activeMonster (m1, 600, 1);
			activeMonster (m2, 400, 0);
		}
	}

	void defense(){
		int m1 = (int)result.x;
		int m2 = (int)result.y;
		int m3 = (int)result.z;

		if (m1 != m2 && m1 != m3 && m2 != m3) { // All Different
			activeDefense (m1, 1);
			activeDefense (m2, 1);
			activeDefense (m3, 1);
		}

		if (m1 == m2 && m2 == m3) { // All the same
			activeDefense (m1, 3);
		}

		if (m1 == m2 && m2 != m3) { // m1 == m2 and m2 != m3
			activeDefense (m1, 2);
			activeDefense (m3, 1);
		}

		if (m1 != m2 && m2 == m3) { // m1 != m2 and m2 == m3
			activeDefense (m1, 1);
			activeDefense (m2, 2);
		}

		if (m1 != m2 && m1 == m3) { // m1 != m2 and m1 == m3
			activeDefense (m1, 2);
			activeDefense (m2, 1);
		}
	}

	void activeDefense(int d, int value){
		Monster[] monsters = Player.transform.GetComponentsInChildren<Monster> ();
		if (d == 0) {
			foreach (Monster m in monsters) {
				if (m.active) {
					m.activeShield (value);
				}
			}
		}
		if (d == 1) {
			Player.GetComponent<Boss> ().activeShield (value);
		}
		if (d == 2) {
			foreach (Monster m in monsters) {
				if (m.active) {
					m.raiseHp (500*value);
				}
			}
		}
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
		ButtonMachine.contButtonClick = 0;
		if (turn == 2) {
			IAControl.startIA = true;
		}
		if (turn > 2) {
			turn = 1;
		}
	}
}
