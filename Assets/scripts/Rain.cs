using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour 
{
	public ParticleSystem particle;
	public int randTime = 3;
	public bool raining = false;
	public bool rainedLast = false;

	public int min = 20;
	public int max = 30;

	void Update () 
	{
		StartCoroutine("Raining");
	}

	private IEnumerator Raining()
	{
		if(!raining)
		{
			raining = true;

			if(rainedLast)
			{
				rainedLast = false;
				particle.enableEmission = false;
			}
			else
			{
				rainedLast = true;
				particle.enableEmission = true;
			}

			randTime = Random.Range(min,max);

			yield return new WaitForSeconds(randTime);

			raining = false;
		}
	}
}
