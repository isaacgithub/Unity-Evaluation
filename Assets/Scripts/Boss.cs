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
			if (bossEnemy.transform.GetChild (i).GetComponent<Monster> ().active)
				enemys.Add (bossEnemy.transform.GetChild (i).gameObject);
		}
		if (enemys.Count > 0) {
			g.transform.position = enemys [Random.Range (0, enemys.Count)].transform.position;
			enemys [Random.Range (0, enemys.Count)].GetComponent<Monster> ().takeDamage (bossAtk);
		} else {
			g.transform.position = bossEnemy.transform.position;
			bossEnemy.GetComponent<Boss>().takeDamage (bossAtk);
		}
		GetComponent<Animator> ().Play ("Attack");

	}
}
