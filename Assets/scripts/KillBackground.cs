﻿using UnityEngine;
using System.Collections;

public class KillBackground : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D ob)
	{

		string obName = ob.gameObject.name;
		GameObject backSpawner = GameObject.Find ("BackgroundSpawner");
		
		BackgroundSpawn spwn = backSpawner.GetComponent<BackgroundSpawn>();

		if (obName == "backLeft(Clone)" || obName == "backRight(Clone)" || obName == "backLeft" || obName == "backRight" || obName == "backLeftspec") 
		{
			spwn.spawnBackground();
			Destroy(ob.gameObject);
		}

		if (obName == "foreLeft(Clone)" || obName == "foreRight(Clone)" || obName == "foreLeft" || obName == "foreRight" || obName == "foreLeftspec") 
		{
			spwn.spawnForeground();
			Destroy(ob.gameObject);
		}

		if (obName == "Cloud1" || obName == "CLoud2" || obName == "Cloud3" || obName == "Cloud1(Clone)" || obName == "CLoud2(Clone)" || obName == "Cloud3(Clone)") 
		{
			spwn.spawnCloud();
			Destroy(ob.gameObject);
		}

	}
}
