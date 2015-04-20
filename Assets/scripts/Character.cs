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
	public Transform blood;
	public Transform coinExplosion;

	public bool walking = false;
	public bool jumping = true;
	public bool sliding = false;
	public bool hasDoubleJump = false;
	public bool grounded = true;
	public bool colliding = true;

	public float runSpeed = 0.9f;
	public float jumpSpeed = 0.9f;
	public float slideSpeed = 0.7f;
	public float jumphi = 200;

	public http httpPost;


	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.name == "GroundCol") 
		{
			jumping = false;			
			walking = false;
			hasDoubleJump = false;
			grounded = true;
			StopCoroutine ("AnimateJump");
			StopCoroutine("AnimateWalk");
			//StartCoroutine ("AnimateWalk");
			
		} 
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		string searchStr;
		if (Random.Range (0, 2) == 1)
			searchStr = "AudioDroneLeft";
		else
			searchStr = "AudioDroneRight";

		GameObject effectAudio = GameObject.Find(searchStr);

		if(collision.gameObject.name == "Crotchet(Clone)")
		{
			effectAudio.GetComponent<effectPlayer>().playEffect();
			collision.gameObject.GetComponent<Renderer>().enabled = false;
			Instantiate(coinExplosion, collision.gameObject.transform.position, Quaternion.identity);
			Destroy (collision.gameObject);
			score.highScore += 100;
		}
		else if ((collision.gameObject.name == "3Blades(Clone)" || collision.gameObject.name == "Blade" || collision.gameObject.name == "2Blades(Clone)" || collision.gameObject.name == "Blade(Clone)" ) && colliding) 
		{
			effectAudio.GetComponent<effectPlayer>().playEffect(0);
			Instantiate(blood, gameObject.transform.position, Quaternion.identity);
			gameObject.GetComponent<Renderer>().enabled = false;
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
			gameObject.GetComponent<Collider2D>().enabled = false;
			GameObject particle = GameObject.Find("Particles");
			Destroy(particle);
			StartCoroutine(endRun());

		}
	}

	private IEnumerator endRun(){
		colliding = false;
		yield return new WaitForSeconds(1f);
		PlayerPrefs.SetInt ("LASTSCORE", score.highScore);
		if(PlayerPrefs.GetInt("HIGHSCORE") < score.highScore)
		{
			PlayerPrefs.SetInt("HIGHSCORE", score.highScore);
		}

		Application.LoadLevel(0);
	}

	void Update()
	{
		///transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);
		Rect bounds = new Rect(0, 0, Screen.width/2, Screen.height);
		if (Input.GetKeyDown (KeyCode.W) || (Input.GetMouseButtonDown(0) && !bounds.Contains(Input.mousePosition)))
		{	
			sliding = false;
			walking =  false;
			grounded = false;
			GameObject player = GameObject.Find ("Player");
			player.GetComponent<Rigidbody2D>().gravityScale = 2;

			if(!jumping)
			{
				GetComponent<Rigidbody2D>().AddForce (Vector3.up * jumphi);
				jumping = true;
				StopCoroutine("AnimateSliding");
				StopCoroutine ("AnimateWalk");
				StartCoroutine ("AnimateJump");
				hasDoubleJump = true;
			}
			else if (jumping && hasDoubleJump){

				GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
				GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumphi / 1.2f);
				jumping = true;
				hasDoubleJump = false;
			}
		} 
		else if((Input.GetKey(KeyCode.S)&&grounded) || ((Input.GetMouseButton(0) && bounds.Contains(Input.mousePosition))&&grounded))
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
				walking = true;
			}
		}
	}

	private IEnumerator AnimateWalk()
	{

		foreach(Sprite step in walk)
		{
			GetComponent<SpriteRenderer>().sprite = step;	
			yield return new WaitForSeconds(runSpeed);
		
		}

		walking = false;
	}

	private IEnumerator AnimateJump()
	{

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

	}

	private IEnumerator AnimateSlide()
	{
		sliding = true;
		walking = false;

		GameObject player = GameObject.Find ("Player");
		BoxCollider2D collider = player.GetComponents<BoxCollider2D>()[0];

		player.GetComponent<Rigidbody2D>().gravityScale = 0;
		collider.size = new Vector2 (collider.size.x + 2f, collider.size.y - 8f);
		collider.offset = new Vector2 (collider.offset.x, -4.61f);

		if (particle != null)
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
		} while (Input.GetKey(KeyCode.S) || Input.GetMouseButton(0));

		foreach(Sprite sl in endSlide)
		{
			GetComponent<SpriteRenderer>().sprite = sl;	
			yield return new WaitForSeconds(slideSpeed);
		}

		if (particle != null)
			particle.enableEmission = false;


		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + .3f, gameObject.transform.position.z);
		collider.offset = new Vector2 (collider.offset.x, -1.207071f);
		collider.size = new Vector2 (collider.size.x - 2f, collider.size.y + 8f);


		player.GetComponent<Rigidbody2D>().gravityScale = 2;
		sliding = false;
		//StartCoroutine ("AnimateWalk");
	}

}
