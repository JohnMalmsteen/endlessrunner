using UnityEngine;
using System.Collections;

public class yAxisRotate : MonoBehaviour {
	
	public int speed = 10;
	
	void Update ()
	{
		transform.Rotate(Vector3.up  * Time.deltaTime * speed);	
	}
}
