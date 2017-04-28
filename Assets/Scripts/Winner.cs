using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour {

	public float max, min;
	public string text;
	public TextMesh msgWinner;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SlotMachine.endGame){
			msgWinner.text = text;
			if (transform.position.y < max)
				transform.Translate (0, 1, 0);
		}  
	}

	public void setText(string t){
		text = t;
	}
}
