using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour 
{
	public int speed = 10;
	
	void Update ()
	{
		transform.Rotate(Vector3.forward  * Time.deltaTime * speed);	
	}
}
