using UnityEngine;
using System.Collections;

public class SpawnBlades : MonoBehaviour 
{
	private static int BLADES_MAX = 3;
	private bool spawning = false;
	
	public GameObject []blades = new GameObject[BLADES_MAX];
	private float spawnTime = 4f;
	public GameObject crotchet;

	void Update () 
	{

		StartCoroutine("Spawn");
	}
	
	private IEnumerator Spawn()
	{
		yield return new WaitForSeconds (spawnTime);
		if (!spawning)
		{
			spawning = true;	
			Vector3 myPos;
			Vector3 crotchPos;

			int randBlade = Random.Range (0, BLADES_MAX);

			if(Random.Range(0, 2) == 1){
				myPos = new Vector3(22f,-1f,-1);
			}
			else{
				myPos = new Vector3(22f,1.75f,-1);
			}

			int randNum = Random.Range(0, 3);
			if(randNum == 1) crotchPos = new Vector3(33f, -.4f, 0);
			else if (randNum == 2) crotchPos = new Vector3(33f, 1.95f, 0);
			else crotchPos = new Vector3(33f, 3.95f, 0);

			Instantiate(blades[randBlade], myPos, Quaternion.identity);
			Instantiate(crotchet, crotchPos, Quaternion.identity);

			spawnTime += Random.Range(-1.7f, 0f);
			yield return new WaitForSeconds (spawnTime);

			spawnTime = 3f;
			spawning = false;
		}
	}
}
