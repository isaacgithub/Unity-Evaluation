using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public int hp;
	public GameObject danoPopUp;
	public GameObject deathAnimation;
	public Transform lifebar;
	private int maxHp;
	private float startSize;
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
			Time.timeScale = 0;
		}
		lifebar.transform.localScale = new Vector3 (startSize*((float)hp/maxHp), lifebar.transform.localScale.y, lifebar.transform.localScale.z);
	}
}
