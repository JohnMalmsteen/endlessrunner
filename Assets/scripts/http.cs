using UnityEngine;
using System.Collections;
using SimpleJSON;

public class http : MonoBehaviour {

	private string postURL = "https://riggscores1337.herokuapp.com/scores";
	private string[] usernames = new string[10]; 
	private string[] scores = new string[10]; 

	public IEnumerator UpdateScores (int score, string username) {
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();

		string secret = Md5Sum (username + score);

		form.AddField("app", secret);
		
		form.AddField("username", username);
		// The score
		form.AddField("score", score);
		// Create a download object
		WWW www = new WWW( postURL, form );
		// Wait until the download is done
		yield return www;

		if(!string.IsNullOrEmpty(www.error)) {
			print( "Error request: " + www.error );
		} else {
			// show the highscores
			var N = JSON.Parse(www.text);

			for(int i = 0; i < 10; i++){
				usernames[i] = N[i]["username"];
				scores[i] = N[i]["score"];
			}

			foreach(var s in scores){
				Debug.Log (s);
			}

			foreach(var u in usernames){
				Debug.Log (u);
			}

		}
	}

	public string Md5Sum(string strToEncrypt){
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
		
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
		
		return hashString.PadLeft(32, '0');
	}
}
