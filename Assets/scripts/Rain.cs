using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour 
{
	public ParticleSystem particle;
	public int randTime = 3;
	public bool raining = false;

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

			particle.enableEmission = !particle.enableEmission;

			randTime = Random.Range(min,max);

			yield return new WaitForSeconds(randTime);

			raining = false;
		}
	}
}
