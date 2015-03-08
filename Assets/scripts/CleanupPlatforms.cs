using UnityEngine;
using System.Collections;

public class CleanupPlatforms : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D ob)
	{
		string obName = ob.gameObject.name;

		GameObject backSpawner = GameObject.Find("BackgroundSpawner");

		SpawnPlatforms spwn = backSpawner.GetComponent<SpawnPlatforms>();
		
		if (obName == "GroundLong(Clone)" || obName == "GroundLong" || obName == "GroundLongspec") 
		{
			spwn.spawnPlatform();
			Destroy(ob.gameObject);
		}
	}
}
