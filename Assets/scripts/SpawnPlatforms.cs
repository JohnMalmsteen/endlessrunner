using UnityEngine;
using System.Collections;

public class SpawnPlatforms : MonoBehaviour 
{
	private static int PLATFORMS_MAX = 6;
	private bool spawning = false;

	public GameObject []platforms = new GameObject[PLATFORMS_MAX];
	public float spawnTime = 1.5f;

	void Update () 
	{
		StartCoroutine("Spawn");
	}

	private IEnumerator Spawn()
	{
		if (!spawning)
		{
			spawning = true;

			int randPlat = Random.Range (0, PLATFORMS_MAX);
			float randHeight;
			Vector3 myPos = new Vector3(11f,-5.624582f,0);

			Instantiate(platforms[randPlat], myPos, Quaternion.identity);

			yield return new WaitForSeconds (spawnTime);

			spawning = false;
		}
	}
}
