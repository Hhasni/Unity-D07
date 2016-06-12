using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TankStatScript : MonoBehaviour {
	
	public int					HP;
	public int					RDamage;
	public int					SDamage;
	public int					Speed;
	public bool 				Death;
	
	public GameObject 			gRocket;
	public ParticleSystem 		psSmoke;
	//public RectTransform		LifeBar;
	public ParticleSystem 		psBurn;
	//private int 						RocketMax;
	private TankStatScript		TankStat;
	//private GameManagerScript			GMScript;

	// Use this for initialization
	void Start () {
		TankStat = GetComponent<TankStatScript> ();
		//Image[] tmpi = GetComponentInChildren<Canvas> ().GetComponentsInChildren<Image> ();
		//LifeBar = tmpi[1].rectTransform;
		ParticleSystem[]  tmp = GetComponentsInChildren<ParticleSystem> ();
		psSmoke = tmp [0];
		psBurn = tmp [1];
		HP = 150;
		RDamage = 50;
		SDamage = 1;
		Speed = 30;
	//	RocketMax = 10;
	}

	
	IEnumerator DestroyAfterExplode(){
		yield return new WaitForSeconds (0.1f);
	//	GMScript.GameOver = true;
		Destroy(this.gameObject);
	}

	void DestoyTank(){
		Death = true;
		if (psSmoke.isPlaying) {
			psSmoke.Play (false);
			psBurn.Play(true);
			psBurn.transform.parent = null;
		}
		GameObject tmp = Instantiate (gRocket, transform.position, Quaternion.identity) as GameObject;
		tmp.transform.parent = null;
		StartCoroutine(DestroyAfterExplode ());
	}

	// Update is called once per frame
	void Update () {
	//	LifeBar.position.x = (HP - 150f);
		if (HP <= 50 && !psSmoke.isPlaying)
			psSmoke.Play (true);
		if (HP <= 0 && !Death) {
			HP = 0;
			DestoyTank ();
		}
	}
}
