using UnityEngine;
using System.Collections;

public class DayNight : MonoBehaviour 
{
	public Color circleSun;
	public Color circleMoon;

	public Color backDay;
	public Color backNight;

	public int slow = 20;		
	public Camera cam;
	Color currentCircle,currentBack;
	SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		currentCircle = Color.Lerp(circleSun, circleMoon, Mathf.PingPong((Time.time) / slow, 1.0f));
		currentBack = Color.Lerp(backDay, backNight, Mathf.PingPong((Time.time) / slow, 1.0f));

		cam.backgroundColor = currentBack;
		spriteRenderer.color = currentCircle;
	}
}
