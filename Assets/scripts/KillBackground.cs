using UnityEngine;
using System.Collections;

public class KillBackground : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D ob)
	{
		string obName = ob.gameObject.name;

		GameObject backSpawner = GameObject.Find("BackgroundSpawner");

		BackgroundSpawn spwn = backSpawner.GetComponent<BackgroundSpawn>();

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

		if (obName == "fogLeft" || obName == "fogLeft(Clone)" || obName == "fogRight" || obName == "fogRight(Clone)") 
		{
			spwn.spawnFog();
			Destroy(ob.gameObject);
		}

	}
}
