using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class score : MonoBehaviour 
{
	public Text scoreText;
	private int highScore;

	void Start()
	{

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
}
