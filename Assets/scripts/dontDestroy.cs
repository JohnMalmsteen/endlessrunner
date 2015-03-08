using UnityEngine;
using System.Collections;

public class dontDestroy : MonoBehaviour {

	private static dontDestroy instance = null;

	public static dontDestroy Instance 
	{
		get { return instance; }
	}
	void Awake() 
	{
		if (instance != null && instance != this) 
		{
			Destroy(this.gameObject);
			return;
		} 
		else 
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}