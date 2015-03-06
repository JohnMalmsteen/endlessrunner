using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	public ParticleSystem particle;
	public Sprite[] walk = new Sprite[6];
	public Sprite[] jump = new Sprite[6];
	public Sprite[] slide = new Sprite[6];

	public bool walking = false;
	public bool jumping = false;
	public bool sliding = false;

	public float runSpeed = 0.9f;
	public float jumpSpeed = 0.7f;
	public float slideSpeed = 0.5f;
	public float jumphi = 200;

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.name == "Ground")
		{
			jumping = false;			
			walking = false;
			StopCoroutine(AnimateJump());
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
				StopCoroutine (AnimateWalk());
				StartCoroutine (AnimateJump());
			}
		} 
		else if(Input.GetKeyDown(KeyCode.S))
		{
			if(!sliding && !jumping)
			{		
				StopCoroutine (AnimateWalk());
				StartCoroutine (AnimateSlide());
			}
		}
		else 
		{
			if (!walking && !jumping) 
			{
				StartCoroutine (AnimateWalk());
			}
		}
	}

	private IEnumerator AnimateWalk()
	{
		walking = true;

		foreach(Sprite step in walk)
		{
			if(!jumping && !sliding){
				GetComponent<SpriteRenderer>().sprite = step;	
				yield return new WaitForSeconds(runSpeed);
			}
			else{
				break;
			}
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
	}

	private IEnumerator AnimateSlide()
	{
		sliding = true;

		particle.enableEmission = true;

		foreach(Sprite sl in slide)
		{
			GetComponent<SpriteRenderer>().sprite = sl;	
			yield return new WaitForSeconds(slideSpeed);
		}

		particle.enableEmission = false;

		sliding = false;
	}

}
