using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour {

	public PauseMenu menu;
	public TextMesh high;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		if(name == "ShareHigh")
			high.text = PlayerPrefs.GetInt ("HighScore").ToString();
	
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
		if (SlotMachine.startGame) {
			if (name == "Pause") {
				PauseMenu.pausedGame = true;
			}
		}
		if (name == "Music") {
			AudioSource m = GameObject.Find ("MusicScene").GetComponent<AudioSource> ();
			if (m.isPlaying)
				m.Stop ();
			else 
				m.Play ();

		}
	}

}
