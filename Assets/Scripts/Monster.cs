using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	public int hp;
	public int atk;
	public int level = 1;
	public int exp = 0;

	public Vector3 startPosition;
	public bool active;
	public float speed;
	public float maxStep;
	public bool battleFase;
	public GameObject danoPopUp;
	private int defaultHp;
	public GameObject deathAnimation;
	public int playerNumber;
	public GameObject atackHit;

	public GameObject[] enemys;
	public GameObject boss;
	// Use this for initialization
	void Start () {
		defaultHp = hp;
		startPosition = transform.localPosition;
		atualizarHUD ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!SlotMachine.endGame) {
			if (active && transform.localPosition.x < maxStep) {
				GetComponent<Animator> ().SetBool ("run", true);
				transform.Translate (speed, 0, 0);
			} else {
				GetComponent<Animator> ().SetBool ("run", false);
			}
			if (battleFase) {
				Invoke ("battle", 1);
				battleFase = false;
			}
		}
	}

	public void battle(){
		int atk_enemy = activeEnemy(); 
		if (atk_enemy == -1)
			atkBoss ();
		else {
			atkEnemy (atk_enemy);
		}
	}

	public void takeDamage(int damage){
		GameObject popUp = Instantiate (danoPopUp);
		popUp.transform.position = transform.position;
		popUp.GetComponent<TextMesh> ().text = damage.ToString ();
		hp -= damage;
		GetComponent<Animator> ().Play ("Hit");
		if (hp <= 0) {
			deathMonster ();
		}
		atualizarHUD ();
	}

	void deathMonster(){
		GameObject death = Instantiate (deathAnimation);
		death.transform.position = transform.position;
		transform.localPosition = startPosition;
		hp = defaultHp;
		active = false;
		exp = 0;
		level = 1;
		transform.FindChild ("Stars").transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;
		transform.FindChild ("Stars").transform.GetChild (2).GetComponent<SpriteRenderer> ().enabled = false;
		if (playerNumber == 2) {
			ScoreControl scr = GameObject.Find ("ScoreControl").GetComponent<ScoreControl> ();
			scr.score+= 10;
			scr.scoreTxt.text = "Score: " + scr.score;
			if (scr.score > PlayerPrefs.GetInt ("HighScore")) {
				PlayerPrefs.SetInt ("HighScore", scr.score);
				scr.highScore.text = "High Score: " + scr.score;
			}
		}

	}

	int activeEnemy(){
		List<int> enemysToAtk = new List<int> ();
		for (int i = 0; i < enemys.Length; i++) {
			if (enemys [i].GetComponent<Monster> ().active) {
				enemysToAtk.Add (i);
			}
		}
		if (enemysToAtk.Count > 0)
			return enemysToAtk [Random.Range (0, enemysToAtk.Count)];
		else
			return -1;
	}

	void atkBoss(){
		GameObject hit = Instantiate (atackHit);
		hit.transform.position = transform.position;
		hit.GetComponent<AtackHit> ().destiny = boss.transform.position;
		hit.GetComponent<AtackHit> ().damage = atk;
		hit.GetComponent<AtackHit> ().enemyDamage = boss;
		battleFase = false;
	}

	void atkEnemy(int enemy){
		Invoke ("raiseExp", 1);
		GameObject enemyToAtk = enemys [enemy];
		GameObject hit = Instantiate (atackHit);
		hit.transform.position = transform.position;
		hit.GetComponent<AtackHit> ().destiny = enemyToAtk.transform.position;
		hit.GetComponent<AtackHit> ().damage = atk;
		hit.GetComponent<AtackHit> ().enemyDamage = enemyToAtk;
		if ((enemyToAtk.GetComponent<Monster> ().hp - atk) <= 0)
			enemyToAtk.GetComponent<Monster> ().active = false;
	}

	void raiseExp(){
		exp += 1;
		Invoke ("addXP", 1);
		if (exp % 3 == 0) {
			raiseLevel ();
		}

	}

	void addXP(){
		popUp ("+1XP");
	}

	public void raiseLevel(){
		popUp ("Level Up!");
		level++;
		if (level == 2) {
			atk += 200;
			hp += 1000;
			transform.FindChild ("Stars").transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = true;
		}
		if (level == 3) {
			atk += 300;
			hp += 2000;
			transform.FindChild ("Stars").transform.GetChild (2).GetComponent<SpriteRenderer> ().enabled = true;
		}
		atualizarHUD ();
	}

	void popUp(string value){
		GameObject popUp = Instantiate (danoPopUp);
		popUp.transform.position = transform.position;
		popUp.GetComponent<TextMesh> ().text = value;
	}

	void atualizarHUD(){
		transform.FindChild ("Window").transform.GetChild (0).GetComponent<TextMesh> ().text = atk + "/" + hp;
	}

	public void raiseHp(int hpRaise){
		popUp ("HP+"+hpRaise);
		hp += hpRaise;
	}

}
