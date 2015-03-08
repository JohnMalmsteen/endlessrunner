using UnityEngine;
using System.Collections;

public class CleanupPlatforms : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D ob)
	{
		string obName = ob.gameObject.name;

		GameObject backSpawner = GameObject.Find("BackgroundSpawner");
		GameObject backKiller = GameObject.Find ("KillBackgroundCloud");
		backKiller.transform.position = new Vector3 (-48.12f, 4.8f, 0);

		SpawnPlatforms spwn = backSpawner.GetComponent<SpawnPlatforms>();
		
		if (obName == "GroundLong(Clone)" || obName == "GroundLong") 
		{
			spwn.spawnPlatform();
			Destroy(ob.gameObject);
		}
	}
}
