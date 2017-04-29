using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public Vector3 startPosition;
	private bool inPause  = false;
	public float max, min;
	public GameObject gray;
	public static bool pausedGame = false;
	// Use this for initialization
	void Start () {
		inPause = false;
		pausedGame = false;
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P) || PauseMenu.pausedGame) {
			PauseMenu.pausedGame = false;
			inPause = !inPause;
		}
		if (inPause) {
			gray.GetComponent<SpriteRenderer> ().enabled = true;
			if (transform.position.y < max)
				transform.Translate (0, 1, 0);
			else
				Time.timeScale = 0;
		} else {
			gray.GetComponent<SpriteRenderer> ().enabled = false;
			if (transform.position.y > min) {
				transform.Translate (0, -1, 0);
				Time.timeScale = 1;
			}
		} 
	}

	public void pauseGame(){
		inPause = true;
	}

	public void resumeGame(){
		inPause = false;
	}

	public void restartGame(){
		inPause = false;
		Application.LoadLevel ("Scene02");
	}
}
