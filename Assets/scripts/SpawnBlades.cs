using UnityEngine;
using System.Collections;

public class SpawnBlades : MonoBehaviour 
{
	private static int BLADES_MAX = 3;
	private bool spawning = false;
	
	public GameObject []blades = new GameObject[BLADES_MAX];
	public float spawnTime = 4f;
	
	void Update () 
	{
		StartCoroutine("Spawn");
	}
	
	private IEnumerator Spawn()
	{
		if (!spawning)
		{
			spawning = true;	
			
			int randBlade = Random.Range (0, BLADES_MAX);
			Vector3 myPos = new Vector3(11f,-1.7f,0);
			
			Instantiate(blades[randBlade], myPos, Quaternion.identity);
			
			yield return new WaitForSeconds (spawnTime);
			
			spawning = false;
		}
	}
}
