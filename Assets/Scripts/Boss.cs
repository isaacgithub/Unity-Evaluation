using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public int hp;
	public int bossAtk;
	public GameObject danoPopUp;
	public GameObject deathAnimation;
	public Transform lifebar;
	private int maxHp;
	private float startSize;
	public GameObject[] vectorAttcks;
	public GameObject bossEnemy;
	public string winnerText;
	public bool shield;
	public GameObject shieldAnimation;
	// Use this for initialization
	void Start () {
		maxHp = hp;
		startSize = lifebar.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void takeDamage(int damage){
		GameObject popUp = Instantiate (danoPopUp);
		popUp.transform.position = transform.position;
		popUp.GetComponent<TextMesh> ().text = damage.ToString ();
		hp -= damage;
		if (hp <= 0) {
			GameObject death = Instantiate (deathAnimation);
			death.transform.position = transform.position;
			Destroy (gameObject);
			FindObjectOfType<Winner> ().setText(winnerText);
			SlotMachine.endGame = true;
		}
		lifebar.transform.localScale = new Vector3 (startSize*((float)hp/maxHp), lifebar.transform.localScale.y, lifebar.transform.localScale.z);
		GetComponent<Animator> ().Play ("Hit");
	}

	public void attack(int atk){
		GameObject g = Instantiate (vectorAttcks [atk]);
		List<GameObject> enemys = new List<GameObject> ();
		for (int i = 0; i < 3; i++) {
			Monster m = bossEnemy.transform.GetChild (i).GetComponent<Monster> ();
			if (m.active)
				enemys.Add (bossEnemy.transform.GetChild (i).gameObject);
		}
		if (enemys.Count > 0) {
			g.transform.position = enemys [Random.Range (0, enemys.Count)].transform.position;
			Monster m = enemys [Random.Range (0, enemys.Count)].GetComponent<Monster> ();
			if(!m.shield)
				m.takeDamage (bossAtk);
			else
				m.desactiveShield();
		} else {
			g.transform.position = bossEnemy.transform.position;
			bossEnemy.GetComponent<Boss>().takeDamage (bossAtk);
		}
		GetComponent<Animator> ().Play ("Attack");

	}

	public void activeShield(int value){
		if (!shield) {
			shield = true;
			GameObject s = Instantiate (shieldAnimation);
			s.transform.position = transform.position;
			s.transform.localScale = new Vector3 (s.transform.localScale.x * 2, s.transform.localScale.y * 2, s.transform.localScale.z * 2);
			s.name = "Shield";
			s.transform.parent = transform;
			if (value >= 2) {
				transform.FindChild ("Shield").transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = true;
			}
			if (value >= 3) {
				transform.FindChild ("Shield").transform.GetChild (2).GetComponent<SpriteRenderer> ().enabled = true;
			}
		}
	}

	public void desactiveShield(){
		if (shield) {
			if (transform.FindChild ("Shield").transform.GetChild (2).GetComponent<SpriteRenderer> ().enabled)
				transform.FindChild ("Shield").transform.GetChild (2).GetComponent<SpriteRenderer> ().enabled = false;
			else if (transform.FindChild ("Shield").transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled)
				transform.FindChild ("Shield").transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;
			else {
				Destroy (transform.FindChild ("Shield").gameObject);
				shield = false;
			}
		}
	}
}
