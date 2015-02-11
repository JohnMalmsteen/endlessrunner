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
		
	void Start ()
	{
		PlayerPrefs.DeleteAll(); // Remove for production

		if(PlayerPrefs.GetInt("App First") != EXIST)
		{		
			init();

			PlayerPrefs.SetInt("App First",EXIST);
		}
	}

	void init()
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

	void updateBoard()
	{
		scr_1.text = PlayerPrefs.GetInt("Score:1").ToString();
		scr_2.text = PlayerPrefs.GetInt("Score:2").ToString();
		scr_3.text = PlayerPrefs.GetInt("Score:3").ToString();
		scr_4.text = PlayerPrefs.GetInt("Score:4").ToString();
		scr_5.text = PlayerPrefs.GetInt("Score:5").ToString();
		scr_6.text = PlayerPrefs.GetInt("Score:6").ToString();
		scr_7.text = PlayerPrefs.GetInt("Score:7").ToString();
		scr_8.text = PlayerPrefs.GetInt("Score:8").ToString();
		scr_9.text = PlayerPrefs.GetInt("Score:9").ToString();
		scr_10.text = PlayerPrefs.GetInt("Score:10").ToString();
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
	}

}
