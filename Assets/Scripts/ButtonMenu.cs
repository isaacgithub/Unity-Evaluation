using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenu : MonoBehaviour {

	public PauseMenu menu;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		if (name == "Resume" || name == "Close") {
			menu.resumeGame ();
		}
		if (name == "Restart") {
			menu.restartGame();
		}
	}

}
