using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public bool			GameOver;
	// Use this for initialization
	void Start () {
	
	}


	IEnumerator Restart(){
		yield return new WaitForSeconds (4f);
		Application.LoadLevel (Application.loadedLevel);
	}

	// Update is called once per frame
	void Update () {
		if (GameOver)
			StartCoroutine (Restart ());
	}
}
