using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : Monster {

	// Update is called once per frame
	void FixedUpdate () {
		if (active && transform.localPosition.x > maxStep) {
			transform.Translate (speed, 0, 0);
		}
		if (battleFase) {
			Invoke ("battle", 1);
			battleFase = false;
		}
	}
}
