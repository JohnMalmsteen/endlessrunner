using UnityEngine;
using System.Collections;

public class start : MonoBehaviour 
{

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel (1);
		}
	}
}
