using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackHit : MonoBehaviour {

	public Vector3 destiny;
	public GameObject enemyDamage;
	public int damage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 move = new Vector3 (destiny.x - transform.position.x, destiny.y - transform.position.y, 0);
		move = move.normalized;
		transform.Translate (move.x * 0.4f, move.y * 0.4f, 0);
		if (!SlotMachine.endGame) {
			if (Vector3.Distance (transform.position, destiny) < 1f) {
				if (enemyDamage.GetComponent<Monster> ())
					enemyDamage.GetComponent<Monster> ().takeDamage (damage);
				else if (enemyDamage.GetComponent<Boss> ()) {
					enemyDamage.GetComponent<Boss> ().takeDamage (damage);
				}
				Destroy (gameObject);
			}
		}
	}
}
