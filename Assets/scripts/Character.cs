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
	public bool jumping = true;
	public bool sliding = false;
	public bool hasDoubleJump = false;
	public bool grounded = true;

	public float runSpeed = 0.9f;
	public float jumpSpeed = 0.9f;
	public float slideSpeed = 0.7f;
	public float jumphi = 200;

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.name == "GroundCol") {
			jumping = false;			
			walking = false;
			hasDoubleJump = false;
			StopCoroutine ("AnimateJump");
			StopCoroutine("AnimateWalk");
			StartCoroutine ("AnimateWalk");

		} 
		else if (collision.gameObject.name == "3Blades(Clone)" || collision.gameObject.name == "Blade" || collision.gameObject.name == "2Blades(Clone)" || collision.gameObject.name == "Blade(Clone)" ) 
		{
			Application.LoadLevel(0);
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy (collision.gameObject);
		score.highScore += 100;
	}

	void Update()
	{
		///transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);

		if (Input.GetKeyDown (KeyCode.W))
		{	
			sliding = false;
			GameObject player = GameObject.Find ("Player");
			StopCoroutine("AnimateSliding");
			StopCoroutine ("AnimateWalk");

			if(!jumping)
			{
				player.rigidbody2D.gravityScale = 2;
				rigidbody2D.AddForce (Vector3.up * jumphi * 1.2f);

				StartCoroutine ("AnimateJump");
				hasDoubleJump = true;
			}
			else if (jumping && hasDoubleJump){
				rigidbody2D.velocity = new Vector3(0, 0, 0);
				rigidbody2D.AddForce(Vector3.up * jumphi / 1.2f);
				//StopCoroutine("AnimateJump");
				//StartCoroutine("AnimateJump");
				hasDoubleJump = false;
			}
		} 
		else if(Input.GetKeyDown(KeyCode.S))
		{
			if(!sliding && !jumping)
			{	
				walking = false;
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
		walking = false;
		jumping = false;

		GameObject player = GameObject.Find ("Player");
		BoxCollider2D collider = player.GetComponents<BoxCollider2D>()[0];

		player.rigidbody2D.gravityScale = 0;
		collider.size = new Vector2 (collider.size.x + 2f, collider.size.y - 8f);
		collider.center = new Vector2 (collider.center.x, -4.61f);

		particle.enableEmission = true;

		foreach (Sprite sl in startSlide) 
		{

			GetComponent<SpriteRenderer>().sprite = sl;
			yield return new WaitForSeconds(slideSpeed);
		}

		do 
		{
			if(!jumping)
			{
				foreach (Sprite sl in heldSlide) 
				{
						GetComponent<SpriteRenderer>().sprite = sl;
						yield return new WaitForSeconds(slideSpeed);
				}
			}else{
				break;
			}
		} while (Input.GetKey(KeyCode.S));

		foreach(Sprite sl in endSlide)
		{
			GetComponent<SpriteRenderer>().sprite = sl;	
			yield return new WaitForSeconds(slideSpeed);
		}

		particle.enableEmission = false;

		collider.center = new Vector2 (collider.center.x, -1.207071f);
		collider.size = new Vector2 (collider.size.x - 2f, collider.size.y + 8f);
		player.rigidbody2D.gravityScale = 2;
		sliding = false;
		//StartCoroutine ("AnimateWalk");
	}

}
