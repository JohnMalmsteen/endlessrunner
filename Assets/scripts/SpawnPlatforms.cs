using UnityEngine;
using System.Collections;

public class SpawnPlatforms : MonoBehaviour 
{
	private static int PLATFORMS_MAX = 6;
	private bool spawning = false;

	public Transform platformBlock;

	public Vector3 spwn_platforms;

	public void spawnPlatform(){
		Instantiate(platformBlock, spwn_platforms, Quaternion.identity);
	}
}
