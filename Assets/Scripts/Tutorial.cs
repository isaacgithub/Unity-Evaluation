using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	public GameObject hand;
	public int clicks = 0;
	public TextMesh msg;
	private Vector3 startSize;
	public GameObject button;
	public int fontSizeDefault;
	public AudioSource audio;

	public Transform[] transfoms;
	// Use this for initialization
	void Start () {
		startSize = button.transform.localScale;
		audio = GameObject.Find ("AudioClick").GetComponent<AudioSource> ();
		fontSizeDefault = msg.fontSize;
		if (SlotMachine.startGame) {
			Destroy (hand.gameObject);
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		audio.Play ();
		if (clicks == 0) {
			hand.transform.position = new Vector3(transfoms [clicks].position.x, transfoms [clicks].position.y, hand.transform.position.z) + new Vector3(0,1.5f,0);
			msg.text = "Change Machine:"+"\n\n"+"- Summon machine"+"\n"+"-Offensive machine"+"\n"+"-Defensive machine";
			msg.fontSize = fontSizeDefault - 5;
		}

		if (clicks == 1) {
			hand.transform.eulerAngles = new Vector3(0,0,-90);
			hand.transform.position = new Vector3(transfoms [clicks].position.x, transfoms [clicks].position.y, hand.transform.position.z) + new Vector3(6,0,0);
			msg.text = "Life bar";
			msg.fontSize = fontSizeDefault;
		}
		if (clicks == 2) {
			hand.transform.eulerAngles = new Vector3(0,0,-90);
			hand.transform.position = new Vector3(transfoms [clicks].position.x, transfoms [clicks].position.y, hand.transform.position.z) + new Vector3(4,-0.5f,0);
			msg.text = "Score";
			msg.fontSize = fontSizeDefault;
		}
		if (clicks == 3) {
			hand.transform.eulerAngles = new Vector3(0,0,90);
			hand.transform.position = new Vector3(transfoms [clicks].position.x, transfoms [clicks].position.y, hand.transform.position.z) + new Vector3(-2,0,0);
			msg.text = "Pause Menu";
			msg.fontSize = fontSizeDefault;
		}
		if (clicks == 4) {
			hand.transform.eulerAngles = new Vector3(0,0,0);
			hand.transform.position = new Vector3(transfoms [clicks].position.x, transfoms [clicks].position.y, hand.transform.position.z) + new Vector3(0,3,0);
			msg.text = "You";
			msg.fontSize = fontSizeDefault;
		}
		if (clicks == 5) {
			hand.transform.eulerAngles = new Vector3(0,0,0);
			hand.transform.position = new Vector3(transfoms [clicks].position.x, transfoms [clicks].position.y, hand.transform.position.z) + new Vector3(0,3,0);
			msg.text = "Enemy";
			msg.fontSize = fontSizeDefault;
		}
		if (clicks == 6) {
			Destroy (hand.gameObject);
			msg.text = "Go!";
			msg.fontSize = fontSizeDefault;
		}
		if (clicks == 7) {
			SlotMachine.startGame = true;
			Destroy (gameObject);
		}
		button.transform.localScale = new Vector3 (button.transform.localScale.x / 1.1f, button.transform.localScale.y / 1.1f, button.transform.localScale.z / 1.3f);
		clicks++;
		Invoke ("defaultSize", 0.1f);
	}

	void defaultSize(){
		button.transform.localScale = startSize;
	}
}
