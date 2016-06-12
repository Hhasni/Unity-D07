using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

	private	ParticleSystem PS;
	// Use this for initialization
	void Start () {
		PS = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PS.isStopped)
			Destroy (this.gameObject);
	
	}
}
