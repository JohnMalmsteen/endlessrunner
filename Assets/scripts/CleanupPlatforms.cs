using UnityEngine;
using System.Collections;

public class CleanupPlatforms : MonoBehaviour 
{
	public GameObject lastCreated;
	void Start()
	{
		lastCreated = GameObject.Find ("GroundLongspec");
	}

	void OnTriggerEnter2D(Collider2D ob)
	{
		string obName = ob.gameObject.name;

		if (obName == "GroundLong(Clone)" || obName == "GroundLong" || obName == "GroundLongspec") 
		{
			ob.transform.position = new Vector3(lastCreated.transform.position.x + 36.5f, -5.59f, 0f);
			lastCreated = ob.gameObject;
		}
	}
}
