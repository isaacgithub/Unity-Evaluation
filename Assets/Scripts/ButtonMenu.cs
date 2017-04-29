using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour {

	public PauseMenu menu;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	
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
		if (name == "Exit") {
			Time.timeScale = 1;
			SceneManager.LoadScene ("Menu");
		}
		if (name == "NewGame") {
			SceneManager.LoadScene ("Scene02");
		}
		if (name == "ExitGame") {
			Application.Quit ();
		}
		if (name == "Pause") {
			PauseMenu.pausedGame = true;
		}
	}

}
