using UnityEngine;
using System.Collections;

public class MoveOffSet : MonoBehaviour {
	private Material currentMaterial;
	public float speed = 1;
	private float offset;
	// Use this for initialization
	void Start () {
		currentMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		offset += Time.deltaTime * 0.05f;
		currentMaterial.SetTextureOffset ("_MainTex", new Vector2 (offset * speed, 0));
	}
}
