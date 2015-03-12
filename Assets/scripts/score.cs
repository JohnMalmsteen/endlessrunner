using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class score : MonoBehaviour 
{
	public Text scoreText;
	public static int highScore;

	void Start()
	{
		highScore = 0;
	}

	void Update () 
	{
		StartCoroutine (scoreme());

		scoreText.text = "Score: " + highScore;
		
		if(PlayerPrefs.GetInt("HIGHSCORE") < highScore)
		{
			PlayerPrefs.SetInt("HIGHSCORE",highScore);
		}
	}

	IEnumerator scoreme()
	{
		yield return new WaitForSeconds(1);

		highScore += 1;
	}

	public void bonus(){
		highScore += 500;
	}
}
