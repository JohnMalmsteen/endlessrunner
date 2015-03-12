using UnityEngine;
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
	void Start()
	{
		highText.text = "High score: " + PlayerPrefs.GetInt("HIGHSCORE");
		lastScore.text = "Last score: " + PlayerPrefs.GetInt ("LASTSCORE");
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
}
