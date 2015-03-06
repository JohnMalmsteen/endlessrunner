using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	public ParticleSystem particle;
	public Sprite[] walk = new Sprite[6];
	public Sprite[] jump = new Sprite[6];
	public Sprite[] startSlide = new Sprite[3];
	public Sprite[] heldSlide = new Sprite[3];
	public Sprite[] endSlide = new Sprite[3];

	public bool walking = false;
	public bool jumping = false;
	public bool sliding = false;
	public bool hasDoubleJump = false;
	public bool grounded = true;

	public float runSpeed = 0.9f;
	public float jumpSpeed = 0.9f;
	public float slideSpeed = 0.7f;
	public float jumphi = 200;

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.name == "Ground")
		{
			jumping = false;			
			walking = false;
			hasDoubleJump = false;
			StopCoroutine("AnimateJump");
			StartCoroutine("AnimateWalk");
		}		
	}

	void Update()
	{
		///transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);

		if (Input.GetKeyDown (KeyCode.W))
		{	
			if(!jumping)
			{
				rigidbody2D.AddForce (Vector3.up * jumphi);
				StopCoroutine ("AnimateWalk");
				StartCoroutine ("AnimateJump");
				hasDoubleJump = true;
			}
			else if (jumping && hasDoubleJump){
				rigidbody2D.velocity = new Vector3(0, 0, 0);
				rigidbody2D.AddForce(Vector3.up * jumphi);
				//StopCoroutine("AnimateJump");
				//StartCoroutine("AnimateJump");
				hasDoubleJump = false;
			}
		} 
		else if(Input.GetKeyDown(KeyCode.S))
		{
			if(!sliding && !jumping)
			{		
				StopCoroutine ("AnimateWalk");
				StartCoroutine ("AnimateSlide");
			}
		}
		else 
		{
			if (!walking && !jumping && !sliding) 
			{
				StopCoroutine("AnimateSlide");
				StartCoroutine("AnimateWalk");
			}
		}
	}

	private IEnumerator AnimateWalk()
	{
		walking = true;

		foreach(Sprite step in walk)
		{
			GetComponent<SpriteRenderer>().sprite = step;	
			yield return new WaitForSeconds(runSpeed);
		
		}

		walking = false;
	}

	private IEnumerator AnimateJump()
	{
		jumping = true;
		
		foreach(Sprite jumpy in jump)
		{
			GetComponent<SpriteRenderer>().sprite = jumpy;	
			yield return new WaitForSeconds(jumpSpeed);
		}

		while (jumping) 
		{
			foreach(Sprite jumpy in jump)
			{
				GetComponent<SpriteRenderer>().sprite = jumpy;	
				yield return new WaitForSeconds(runSpeed);
			}
		}
		jumping = false;
	}

	private IEnumerator AnimateSlide()
	{
		sliding = true;

		particle.enableEmission = true;

		foreach (Sprite sl in startSlide) 
		{
			GetComponent<SpriteRenderer>().sprite = sl;
			yield return new WaitForSeconds(slideSpeed);
		}

		do 
		{
			foreach (Sprite sl in heldSlide) 
			{
				GetComponent<SpriteRenderer>().sprite = sl;
				yield return new WaitForSeconds(slideSpeed);
			}
		} while (Input.GetKey(KeyCode.S));

		foreach(Sprite sl in endSlide)
		{
			GetComponent<SpriteRenderer>().sprite = sl;	
			yield return new WaitForSeconds(slideSpeed);
		}

		particle.enableEmission = false;

		sliding = false;
		StartCoroutine ("AnimateWalk");
	}

}
