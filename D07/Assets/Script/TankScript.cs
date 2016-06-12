using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TankScript : MonoBehaviour {

	public GameObject 					gCanon;
	public GameObject 					gCat;
	public GameObject 					gFire;
	public GameObject 					gRocket;
	public Image 						my_cross;

	public Rigidbody 					RBTank;
	public float 						MoveSpeed;
	public float 						RotSpeed;
	public float 						Boost;

	public bool 						Death;
	private int 						BoostMax;
	private int 						RocketMax;
	private float 						timerShoot;
	private TankStatScript				TankStat;
	private GameManagerScript			GMScript;
	private GuiLifeBar					myGuiScript;

	// Use this for initialization
	void Start () {
		TankStat = GetComponent<TankStatScript> ();
		GMScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameManagerScript> ();
		MoveSpeed = 10;
		RotSpeed = 100;
		Boost = 0;
		RocketMax = 10;
		BoostMax = 50;
		RBTank = GetComponent<Rigidbody> ();
		myGuiScript = GetComponentInChildren<GuiLifeBar> ();
	}
	
	void fire(){
		if (timerShoot > 0.1f) {
			timerShoot = 0;
			RaycastHit hit;
			if (Physics.Raycast (transform.position, gCanon.transform.TransformDirection (Vector3.forward), out hit, 30)) {
				Instantiate (gFire, hit.point, Quaternion.identity);
				if (hit.collider.gameObject.tag == "Player"){
					hit.collider.gameObject.GetComponentInParent<TankStatScript>().HP -= TankStat.SDamage;
					my_cross.color = Color.red;
					Invoke("CrossWhite", 0.18f);
				}
			}
		}
	}

	void CrossWhite(){
		if (my_cross.color == Color.red)
			my_cross.color = Color.white;
	}

	void rocket(){

		//myGuiScript.RefreshRocket (RocketMax);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, gCanon.transform.TransformDirection (Vector3.forward), out hit, 30)) {
			Instantiate (gRocket, hit.point, Quaternion.identity);
			RocketMax -= 1;
			myGuiScript.RefreshRocket (RocketMax);
			if (hit.collider.gameObject.tag == "Player") {
				hit.collider.gameObject.GetComponentInParent<TankStatScript> ().HP -= TankStat.RDamage;
				my_cross.color = Color.red;
				Invoke("CrossWhite", 0.1f);
			}
		}
	}

	void resetMoveSpeed(){
		MoveSpeed -= 20;
	}

	void GetInput(){
		if (Input.GetKey (KeyCode.LeftShift) && Boost >= BoostMax && (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S))) {
			MoveSpeed += 20;
			Boost = 0;
			Invoke("resetMoveSpeed", 5f);
		}
		if (Input.GetKey (KeyCode.W))
			RBTank.MovePosition(transform.position + gCat.transform.forward * MoveSpeed * Time.deltaTime);
		if (Input.GetKey (KeyCode.S))
			RBTank.MovePosition (transform.position - gCat.transform.forward * MoveSpeed * Time.deltaTime);
		if (Input.GetKey(KeyCode.A))
			gCat.transform.Rotate (0, -1 * Time.deltaTime * RotSpeed, 0);
		if (Input.GetKey(KeyCode.D))
			gCat.transform.Rotate (0, 1 * Time.deltaTime * RotSpeed, 0);
		gCanon.transform.Rotate (0, Input.GetAxis ("Mouse X") * Time.deltaTime * RotSpeed, 0);
		if (Input.GetMouseButton (0))
			fire ();
		if (Input.GetMouseButtonDown (1) && RocketMax > 0)
			rocket ();

	}
	
	// Update is called once per frame
	void Update () {
		if (TankStat.Death) {
			GMScript.GameOver = true;
			Camera.main.transform.parent = null;
			return;
		}
		GetInput ();
		if (Boost < BoostMax)
			Boost += Time.deltaTime;
		timerShoot += Time.deltaTime;
	}
}
