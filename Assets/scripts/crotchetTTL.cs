using UnityEngine;
using System.Collections;

public class crotchetTTL : MonoBehaviour {

	public float timeToLive = 4f;

	// Use this for initialization
	void Start () {
		StartCoroutine ("TTL");
	}

	// Update is called once per frame
	private IEnumerator TTL(){
		yield return new WaitForSeconds(timeToLive);
		Destroy (gameObject);
	}
}
