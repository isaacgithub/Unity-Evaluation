using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : MonoBehaviour {


	public int score = 0;
	public TextMesh scoreTxt;
	public TextMesh highScore;
	// Use this for initialization
	void Start () {
		score = 0;
		highScore.text = "High Score: " + PlayerPrefs.GetInt ("HighScore");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
