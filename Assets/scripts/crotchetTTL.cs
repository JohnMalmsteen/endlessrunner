using UnityEngine;
using System.Collections;

public class crotchetTTL : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ("TTL");
	}
	
	// Update is called once per frame
	private IEnumerator TTL(){
		yield return new WaitForSeconds(4f);
		Destroy (gameObject);
	}
}
