using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	public Sprite[] walk = new Sprite[4];
	public bool walking = true;
	public float speed = 0.3f;

	void Awake()
	{
		gameObject.AddComponent("BoxCollider2D");
	}

	void Update()
	{
		if(walking)
		{
			StartCoroutine(Animate());
		}

		if (Input.GetKeyDown (KeyCode.D))
		{
			transform.rotation = Quaternion.Euler(0,180,0);
			rigidbody2D.AddForce(Vector3.up * 100);
		}
		else if(Input.GetKeyDown (KeyCode.A))
		{
			transform.rotation = Quaternion.Euler(0,0,0);
			rigidbody2D.AddForce(Vector3.up * 100);
		}
	}

	private IEnumerator Animate()
	{
		walking = false;

		foreach(Sprite step in walk)
		{
			GetComponent<SpriteRenderer>().sprite = step;	
			yield return new WaitForSeconds(speed);
		}

		walking = true;
	}

}
