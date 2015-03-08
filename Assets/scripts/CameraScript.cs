using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform player;

	void Start(){
		GameObject audio = GameObject.Find ("AudioDrone");
		audio.transform.position = transform.position;

	}
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.position.x + 12, (float)((player.position.y+7.5f) - (player.position.y+6.5f)/1.7f), -10);
		GameObject audio = GameObject.Find ("AudioDrone");
		audio.transform.position = transform.position;
	}
}
