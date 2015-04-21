using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour 
{
	private const int EXIST = 1; 

	public Text scr_1;
	public Text scr_2;
	public Text scr_3;
	public Text scr_4;
	public Text scr_5;
	public Text scr_6;
	public Text scr_7;
	public Text scr_8;
	public Text scr_9;
	public Text scr_10;

	private string[] usernames = new string[10];
	private string[] scores = new string[10];
		
	void Start (){
		Debug.Log("hey");
		usernames = PlayerPrefsX.GetStringArray ("Names");
		scores = PlayerPrefsX.GetStringArray ("Scores");

		updateBoard ();
	}

	void updateBoard(){
		scr_1.text = usernames[0] + " : " + scores[0];
		scr_2.text = usernames[1] + " : " + scores[1];
		scr_3.text = usernames[2] + " : " + scores[2];
		scr_4.text = usernames[3] + " : " + scores[3];
		scr_5.text = usernames[4] + " : " + scores[4];
		scr_6.text = usernames[5] + " : " + scores[5];
		scr_7.text = usernames[6] + " : " + scores[6];
		scr_8.text = usernames[7] + " : " + scores[7];
		scr_9.text = usernames[8] + " : " + scores[8];
		scr_10.text = usernames[9] + " : " + scores[9];
	}

	/*void init()
	{
		int j = 1;

		for(int i = 0;i < 10; i++)
		{
			string varscore = "Score:" + j.ToString();

			PlayerPrefs.SetInt(varscore,1000 * j);

			j++;
		}

		setScore(10001);

		printScore();

		updateBoard();
	}



	void setScore(int score)
	{
		int j = 1;
		int varscore;

		ArrayList list = new ArrayList();

		list.Add(score);

		for(int i = 0;i < 10; i++)
		{
			varscore = PlayerPrefs.GetInt("Score:" + j.ToString());

			list.Add(varscore);
			
			j++;
		}		
				
		list.Sort();

		for(int i = 0;i < 11; i++)
		{
			int x = (int)(list[i]);

			PlayerPrefs.SetInt(("Score:" + i.ToString()),x);
		}

	}

	void printScore()
	{		
		int j = 10;
		
		for(int i = 10;i > 0; i--)
		{
			string varscore = "Score:" + j.ToString();			
			
			print(PlayerPrefs.GetInt(varscore));
			
			j--;
		}
	}*/

}
