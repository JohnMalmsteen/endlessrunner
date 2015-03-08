using UnityEngine;
using System.Collections;

public class SpawnPlatforms : MonoBehaviour 
{
	private static int PLATFORMS_MAX = 6;
	private bool spawning = false;

	public Transform platformBlock;
	public GameObject lastCreated;
	public Vector3 spwn_platforms;

	void Start(){
		lastCreated = GameObject.Find ("GroundLongspec");
	}

	public void spawnPlatform()
	{
		lastCreated = ((Transform)Instantiate(platformBlock, new Vector3(lastCreated.transform.position.x + 36.5f, -5.59f, 0f), Quaternion.identity)).gameObject;
	}
}
