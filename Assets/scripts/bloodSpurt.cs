using UnityEngine;
using System.Collections;

public class bloodSpurt : MonoBehaviour 
{
	public int min = 200;
	public int max = 500;

	void Start () 
	{
		int force = Random.Range (min, max);
		int dir = Random.Range (0, 4);

		switch (dir)
		{
			case 0:
				rigidbody2D.AddForce(Vector3.up * force);
				break;

			case 1:
				rigidbody2D.AddForce(Vector3.down * force);
				break;

			case 2:
				rigidbody2D.AddForce(Vector3.left * force);
				break;

			case 3:
				rigidbody2D.AddForce(Vector3.right * force);
				break;
		}

		StartCoroutine(kill());
	}

	IEnumerator kill()
	{
		yield return new WaitForSeconds(2);

		Destroy(gameObject);
	}
}
