using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public	static SoundManager	instance{get; private set; }
	public	AudioSource 	aShoot;
	public	AudioSource 	aRocket;
	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayShoot(){
		if (!aShoot.isPlaying)
			aShoot.Play();
	}

	public void PlayRocket(){
		if (!aShoot.isPlaying)
			aShoot.Play();
	}
}
