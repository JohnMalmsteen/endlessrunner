using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform player;
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.position.x + 6, (float)((player.position.y+6.5f) - (player.position.y+6.5f)/1.7f), -10);
	}
}
