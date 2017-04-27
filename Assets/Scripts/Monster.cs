using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	public int hp;
	public int atk;
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
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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
		GetComponent<Animator> ().SetBool ("hit", true);
		Invoke ("desactiveHit", 0.2f);
		if (hp <= 0) {
			GameObject death = Instantiate (deathAnimation);
			death.transform.position = transform.position;
			transform.localPosition = startPosition;
			hp = defaultHp;
			active = false;
		}
	}

	void desactiveHit(){
		GetComponent<Animator> ().SetBool ("hit", false);
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
		GameObject enemyToAtk = enemys [enemy];
		GameObject hit = Instantiate (atackHit);
		hit.transform.position = transform.position;
		hit.GetComponent<AtackHit> ().destiny = enemyToAtk.transform.position;
		hit.GetComponent<AtackHit> ().damage = atk;
		hit.GetComponent<AtackHit> ().enemyDamage = enemyToAtk;
		if ((enemyToAtk.GetComponent<Monster> ().hp - atk) <= 0)
			enemyToAtk.GetComponent<Monster> ().active = false;
	}


}
