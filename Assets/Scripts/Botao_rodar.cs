using UnityEngine;
using System.Collections;

public class Botao_rodar : MonoBehaviour {

	public bool clicou;
	public int dir;
	public float rotate_speed;//0 a 1
	private float tempo;

	// Use this for initialization
	void Start () 
	{
		/*#if UNITY_ANDROID && !UNITY_EDITOR
		//send notification here
		AndroidJavaObject ajc = new AndroidJavaObject("com.zeljkosassets.notifications.Notifier");
		ajc.CallStatic("sendNotification", "Unity Evaluation", "Unity Evaluation", "Unity Evaluation", 5);
		#endif*/
		clicou = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(new Vector3(0,0,dir*Mathf.Sin(rotate_speed)));	
		if(Mathf.Abs(transform.rotation.z)>=0.05)
		{
			rotate_speed = -rotate_speed;
		}
	}

	void OnMouseDown()
	{
		clicou = true; 
	}

	void OnMouseUp()
	{
		clicou = false;
	}
}
