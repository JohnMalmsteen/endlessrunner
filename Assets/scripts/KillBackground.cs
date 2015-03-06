using UnityEngine;
using System.Collections;

public class KillBackground : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D ob)
	{
		string obName = ob.gameObject.name;

		GameObject backSpawner = GameObject.Find("BackgroundSpawner");
		GameObject cloudSpawner = GameObject.Find("CloudSpawner");

		BackgroundSpawn spwn = backSpawner.GetComponent<BackgroundSpawn>();
		Cloud cloud = backSpawner.GetComponent<Cloud>();

		if (obName == "mountainLeft" || obName == "backLeft(Clone)" || obName == "backRight(Clone)" || obName == "backLeft" || obName == "backRight") 
		{
			spwn.spawnBackground();
			Destroy(ob.gameObject);
		}

		if (obName == "nearMountains" || obName == "foreLeft(Clone)" || obName == "foreRight(Clone)" || obName == "foreLeft" || obName == "foreRight") 
		{
			spwn.spawnForeground();
			Destroy(ob.gameObject);
		}

		if (obName == "fog" || obName == "fogLeft" || obName == "fogLeft(Clone)" || obName == "fogRight" || obName == "fogRight(Clone)") 
		{
			spwn.spawnFog();
			Destroy(ob.gameObject);
		}

		if (obName == "cloud_1(Clone)" || obName == "cloud_2(Clone)" || obName == "cloud_3(Clone)") 
		{
			cloud.spawnCloud();
			Destroy(ob.gameObject);
		}

	}
}
