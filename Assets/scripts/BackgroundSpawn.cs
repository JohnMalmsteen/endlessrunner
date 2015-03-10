using UnityEngine;
using System.Collections;

public class BackgroundSpawn : MonoBehaviour
{
	public GameObject lastSpawnedBackGround;
	public GameObject lastSpawnedForeGround;
	public Transform backgroundLeft;
	public Transform foregroundLeft;
	public Transform backgroundRight;
	public Transform foregroundRight;
	public Transform cloud1;
	public Transform cloud2;
	public Transform cloud3;

	public Vector3 spwn_cloud_high;
	public Vector3 spwn_cloud_low;
	public Vector3 spwn_cloud_mid;
	
	private bool foreLeft = true;
	private bool backLeft = true;


	void Start(){
		lastSpawnedBackGround = GameObject.Find ("backLeftspec");
		lastSpawnedForeGround = GameObject.Find ("foreLeftspec");
		spawnBackground ();
		spawnForeground ();
	}

	public void spawnBackground()
	{
		Transform backgroundTransform;

		if (backLeft)
			backgroundTransform = backgroundRight;
		else
			backgroundTransform = backgroundLeft;

		lastSpawnedBackGround = ((Transform)Instantiate(backgroundTransform, new Vector3(lastSpawnedBackGround.transform.position.x + 32.63f, lastSpawnedBackGround.transform.position.y, 0f), Quaternion.identity)).gameObject;

		backLeft = !backLeft;
	}

	public void spawnForeground()
	{
		Transform foregroundTransform;
		
		if (foreLeft)
			foregroundTransform = foregroundRight;
		else
			foregroundTransform = foregroundLeft;
		
		lastSpawnedForeGround = ((Transform)Instantiate(foregroundTransform, new Vector3(lastSpawnedForeGround.transform.position.x + 30.63f, lastSpawnedForeGround.transform.position.y, 0f), Quaternion.identity)).gameObject;
		
		foreLeft = !foreLeft;
	}



	public void spawnCloud()
	{
		int selector = Random.Range (0, 2);
		
		Vector3 [] vecs = {spwn_cloud_high, spwn_cloud_mid, spwn_cloud_low};
		
		switch (selector) {
		case 0:
			Instantiate(cloud1, vecs[Random.Range(0, 2)], Quaternion.identity);
			break;
		case 1:
			Instantiate(cloud2, vecs[Random.Range(0, 2)], Quaternion.identity);
			break;
		case 2:
			Instantiate(cloud3, vecs[Random.Range(0, 2)], Quaternion.identity);
			break;
		}
	}
}
