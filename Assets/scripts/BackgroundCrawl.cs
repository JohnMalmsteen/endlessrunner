using UnityEngine;
using System.Collections;

public class BackgroundCrawl : MonoBehaviour
{
	public float speed = 0.003f;

	void Update () 
	{
		transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
	}
}
