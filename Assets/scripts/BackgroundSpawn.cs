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
	public Transform cloud1;
	public Transform cloud2;
	public Transform cloud3;

	public Vector3 spwn_back;
	public Vector3 spwn_fore;
	public Vector3 spwn_fog;
	public Vector3 spwn_cloud_high;
	public Vector3 spwn_cloud_low;
	public Vector3 spwn_cloud_mid;
	
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

	public void spawnCloud()
	{
		int selector = Random.Range (0, 2);
		
		Vector3 [] vecs = {spwn_cloud_high, spwn_cloud_mid, spwn_cloud_low};
		
		switch (selector) {
		case 0:
			Instantiate(cloud1, vecs[Random.Range(0, 2)], Quaternion.identity);
			break;
		case 1:
			Instantiate(cloud2, vecs[Random.Range(0, 2)], Quaternion.identity);
			break;
		case 2:
			Instantiate(cloud3, vecs[Random.Range(0, 2)], Quaternion.identity);
			break;
		}
	}
}
