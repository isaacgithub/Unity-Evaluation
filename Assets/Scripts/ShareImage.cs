﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class ShareImage : MonoBehaviour {

	 private bool isProcessing = false;
 	 
	 private string shareText  = "Can you beat my record?\n";
	 private string gameLink = "Download the game on play store at "+"\nhttps://play.google.com/Unity-Evaluation";
	 private string subject = "Unity Evaluation";
	 public TextAsset imageTA;
	 public void shareImage(){
	  if(!isProcessing)
	    StartCoroutine( ShareScreenshot() );
	 }
	 
	 private IEnumerator ShareScreenshot(){
		
	    isProcessing = true;
	    yield return new WaitForEndOfFrame();
		var width = Screen.width;
		var height = Screen.height;
		Texture2D tex=new Texture2D(width,height,TextureFormat.RGB24,false);
		tex.ReadPixels(new Rect(0,0,width,height),0,0);
        tex.Apply();

	    byte[] dataToSave = tex.EncodeToPNG();
	 
	  	string destination = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
	 	File.WriteAllBytes(destination, dataToSave);
	 
		if (!Application.isEditor) {
			AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject> ("parse", "file://" + destination);
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_STREAM"), uriObject);
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_TEXT"), shareText + gameLink);
			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_SUBJECT"), subject);
			intentObject.Call<AndroidJavaObject> ("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");
	
			currentActivity.Call ("startActivity", intentObject);
		}
		
	  
	   
	  isProcessing = false;
	 
	 }

	void OnMouseDown(){
		shareImage ();
	}

}
