using UnityEngine;
using System.Collections;

public class Mobile : MonoBehaviour 
{
	void Start () 
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.Quit();
		}
	}
}
