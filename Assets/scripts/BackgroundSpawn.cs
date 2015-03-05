using UnityEngine;
using System.Collections;

public class BackgroundSpawn : MonoBehaviour
{
	public Transform backgroundLeft;
	public Transform foregroundLeft;
	public Transform fogLeft;
	public Transform backgroundRight;
	public Transform foregroundRight;
	public Transform fogRight;

	public Vector3 spwn_back;
	public Vector3 spwn_fore;
	public Vector3 spwn_fog;

	private bool foreLeft = true;
	private bool backLeft = true;
	private bool fgLeft = true;

	public void spawnBackground()
	{
		if(backLeft)
		{
			Instantiate(backgroundRight, spwn_back, Quaternion.identity);

			backLeft = false;
		}
		else
		{
			Instantiate(backgroundLeft, spwn_back, Quaternion.identity);

			backLeft = true;
		}
	}

	public void spawnForeground()
	{
		if (foreLeft) 
		{			
			Instantiate (foregroundRight, spwn_fore, Quaternion.identity);

			foreLeft = false;
		}
		else
		{				
			Instantiate(foregroundLeft, spwn_fore, Quaternion.identity); 

			foreLeft = true;
		}
	}

	public void spawnFog()
	{
		if (fgLeft) 
		{			
			Instantiate (fogRight, spwn_fog, Quaternion.identity);
			
			fgLeft = false;
		}
		else
		{				
			Instantiate(fogLeft, spwn_fog, Quaternion.identity); 
			
			fgLeft = true;
		}
	}
}
