using UnityEngine;
using System.Collections;

public class effectPlayer : MonoBehaviour {

	public AudioClip [] effects = new AudioClip[10];


	public void playEffect(int i)
	{
		AudioSource eAudio = gameObject.GetComponent<AudioSource>();
		eAudio.volume = 100;
		eAudio.PlayOneShot (effects [i]);
	}

	public void playEffect()
	{
		AudioSource eAudio = gameObject.GetComponent<AudioSource>();
		eAudio.PlayOneShot(effects[Random.Range(1, effects.Length)]);
	}
}
