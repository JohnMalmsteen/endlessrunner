using UnityEngine;
using System.Collections;

public class http : MonoBehaviour {

	private string postURL = "https://riggscores1337.herokuapp.com/scores";
	private string username = "XXX";
	private int score = 777777;

	private string _player = "";
	
	public IEnumerator CloudThing (int endScore) {
		Debug.Log ("Ienumerator");
		Debug.Log (score);
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		
		form.AddField( "app", "true" );
		
		form.AddField( "username", username );
		// The score
		form.AddField( "score", score );
		Debug.Log ("Form");
		// Create a download object
		WWW www = new WWW( postURL, form );
		// Wait until the download is done
		yield return www;
		Debug.Log ("Yield");
		if(!string.IsNullOrEmpty(www.error)) {
			print( "Error request: " + www.error );
		} else {
			// show the highscores
			Debug.Log(www.text);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
