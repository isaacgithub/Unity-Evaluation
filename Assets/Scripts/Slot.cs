using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

	private Material currentMaterial;
	public float speed = 1;
	public float offset;
	public bool stopMachine;
	// Use this for initialization
	void Start () {
		stopMachine = true;
		currentMaterial = GetComponent<Renderer>().material;
		currentMaterial.SetTextureOffset ("_MainTex", new Vector2 (0, offset));
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!stopMachine) {
			offset += Time.deltaTime * 0.05f;
			currentMaterial.SetTextureOffset ("_MainTex", new Vector2 (0, offset * speed));
		}
	}
}
