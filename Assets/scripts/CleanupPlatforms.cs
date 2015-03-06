using UnityEngine;
using System.Collections;

public class CleanupPlatforms : MonoBehaviour 
{
	void Start () 
	{
		StartCoroutine("clean");
	}

	private IEnumerator clean()
	{
		yield return new WaitForSeconds(5);
		
		Destroy (gameObject);
	}
}
