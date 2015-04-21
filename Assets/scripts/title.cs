﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class title : MonoBehaviour 
{
	public GameObject titleChar;
	public Sprite[] slide = new Sprite[3];
	public float slideSpeed = 0.03f;
	public bool sliding = false;
	public Text highText;
	public Text lastScore;
	public http httpPost;

	void Start()
	{
		Debug.Log ("title start");
		httpPost = gameObject.AddComponent <http>() as http;
		//int endScore = PlayerPrefs.GetInt("HIGHSCORE");
		int endScore = 888890;
		string username = "aaa";
		StartCoroutine(httpPost.UpdateScores(endScore, username));

		highText.text = "High score: " + PlayerPrefs.GetInt("HIGHSCORE");
		lastScore.text = "Last score: " + PlayerPrefs.GetInt("LASTSCORE");
	}

	void Update()
	{
		StartCoroutine(AnimateSlide());
	}

	
	private IEnumerator AnimateSlide()
	{	
		if (!sliding)
		{
			sliding = true;
			
			foreach (Sprite sl in slide)
			{
				titleChar.GetComponent<SpriteRenderer> ().sprite = sl;
				yield return new WaitForSeconds (slideSpeed);
			}
			
			sliding = false;
		}
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void PlayGame()
	{
		Application.LoadLevel(1);
	}

	public void GoCredits()
	{
		Application.LoadLevel(2);
	}


}

