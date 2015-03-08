using UnityEngine;
using System.Collections;

public class DestroyBlades : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D ob)
	{
		string obName = ob.gameObject.name;

		GameObject bladeKiller = GameObject.Find ("BladeKiller");
		bladeKiller.transform.position = new Vector3 (-28.7f, -1.1f, 0);

		if (obName == "Blade" || obName == "Blade(Clone)" || obName == "2Blades(Clone)" || obName == "3Blades(Clone)") 
		{
			Destroy(ob.gameObject);
		}
	}
}
