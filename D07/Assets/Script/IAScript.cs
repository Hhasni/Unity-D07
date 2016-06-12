using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IAScript : MonoBehaviour {

	
	public GameObject 					gCat;
	public GameObject 					gCanon;
	public GameObject 					gTarget;
	public GameObject 					gFire;
	public GameObject 					gRocket;
	public NavMeshAgent 				nmAgent;
	public TankStatScript 				TankStat;


	public List <GameObject> 			lTargets;

	public bool 						isReach;
	
	public float						ShooTimer;
	public float						RockeTimer;
	public float						NextShoot;
	public int							NextRocket;

	// Use this for initialization
	void Awake () {
		TankStat = GetComponent<TankStatScript> ();
		nmAgent = GetComponent<NavMeshAgent> ();
		lTargets =  new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
		lTargets.ForEach (SearchDoublon);
		gCat = transform.GetChild (0).gameObject;
		gCanon = transform.GetChild (1).gameObject;
		NextShoot = 1;
		NextShoot = 0.1f;
		lTargets.ForEach (SearchDoublon);
	}
	void Start(){//
	}

	void SearchDoublon(GameObject obj){//
		if (obj.transform.parent != null)
			lTargets.Remove (obj);
		if (obj.GetInstanceID () == this.gameObject.GetInstanceID ())
			lTargets.Remove (obj);
		if (obj.GetInstanceID () == gCat.GetInstanceID ())
			lTargets.Remove (obj);
	}

	void FindNear(GameObject obj){//
		if (obj == null) {
			lTargets.Remove (obj);
			return;
		}
		float tmp = Vector3.Distance (this.transform.position, obj.transform.position);
		if (!gTarget && obj)
			gTarget = obj;
		if (tmp < Vector3.Distance (this.transform.position, gTarget.transform.position))
			gTarget = obj;


	}
	
	void getTarget(){//
		lTargets.ForEach (FindNear);
		isReach = false;
		nmAgent.updatePosition = true;
	}
	
	void fire(RaycastHit hit){
		Instantiate (gFire, hit.point, Quaternion.identity);
		if (hit.collider.gameObject.tag == "Player")
			hit.collider.gameObject.GetComponentInParent<TankStatScript>().HP  -= TankStat.SDamage;
	}

	void rocket(RaycastHit hit){
		Instantiate (gRocket, hit.point, Quaternion.identity);
		if (hit.collider.gameObject.tag == "Player")
			hit.collider.gameObject.GetComponentInParent<TankStatScript>().HP  -= TankStat.RDamage;
	}

	void OnTriggerStay(Collider col){
		if (nmAgent.updatePosition) {
			if (col.gameObject.tag == "Player" && col.gameObject.transform.parent.gameObject.GetInstanceID () == gTarget.GetInstanceID ()) {
				Debug.Log (col.gameObject.tag + "" + col.gameObject.name);// && col.gameObject.transform.parent.gameObject.GetInstanceID () == gTarget.GetInstanceID ()) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position, gCanon.transform.TransformDirection (Vector3.forward), out hit, 29)) {
					nmAgent.updatePosition = false;
					isReach = true;
					nmAgent.SetDestination (transform.position);
				}
			}
		}
		if (Random.Range (0, 2) == 1) {
			if (RockeTimer > NextRocket) {
				RockeTimer = 0;
				NextRocket = Random.Range (3, 9);
				Debug.Log (col.gameObject.tag + " " + col.gameObject.name);
				Debug.Log (col.gameObject.name);
				if (col.gameObject.tag == "Player") {// && col.gameObject.transform.parent.gameObject.GetInstanceID () == gTarget.GetInstanceID ()) {
					RaycastHit hit;
					if (Physics.Raycast (transform.position, gCanon.transform.TransformDirection (Vector3.forward), out hit, 30)) {
						rocket (hit);
					}
				}
			}
		}
		else{
			if (ShooTimer > NextShoot) {
				ShooTimer = 0;
				NextShoot = Random.Range(0.1f , 0.5f);
				if (col.gameObject.tag == "Player") {// && col.gameObject.transform.parent.gameObject.GetInstanceID () == gTarget.GetInstanceID ()) {
					RaycastHit hit;
					if (Physics.Raycast (transform.position, gCanon.transform.TransformDirection (Vector3.forward), out hit, 30)) {
						fire (hit);
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player" && col.gameObject.GetInstanceID() != gCat.GetInstanceID()) {
			if (gTarget){
				if (Vector3.Distance (this.transform.position,col.gameObject.transform.position) < 
			    	Vector3.Distance (this.transform.position, gTarget.transform.position));{
					gTarget = col.gameObject.transform.parent.gameObject;
					nmAgent.updatePosition = true;
					isReach = false;
				}
			}
			else{
				gTarget = col.gameObject.transform.parent.gameObject;
			}
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			if (!gTarget)
				getTarget ();
			else{
				isReach = false;
				nmAgent.updatePosition = true;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (gTarget)
			transform.LookAt (gTarget.transform.position);
		else
			getTarget ();
		if (gTarget && !isReach)
			nmAgent.SetDestination (gTarget.transform.position);
		ShooTimer += Time.deltaTime;
		RockeTimer += Time.deltaTime;
	}
}
