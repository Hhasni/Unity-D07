using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiLifeBar : MonoBehaviour {
	
	public	Text				myText;
	public	GameObject			gPlayer;
	private RectTransform 		RtPos;
	private TankStatScript		TankStat;
	public 	Vector3				vOriPos;
	public 	enum Type {Player, Other};
	public 	Type type;

	// Use this for initialization
	void Start () {
		if (type == Type.Player)
			myText = transform.parent.parent.GetComponentInChildren<Text> ();
		RtPos = GetComponent<RectTransform> ();
		TankStat = GetComponentInParent<TankStatScript> ();
		vOriPos = RtPos.localPosition;
	}

	public void RefreshRocket(int nb){
		myText.text = nb.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		if (type == Type.Other && gPlayer)
			gameObject.transform.parent.LookAt (gPlayer.transform);
		if (TankStat.HP > 0)
			RtPos.localPosition = new Vector3 ((TankStat.HP / 1.5F) - 100, vOriPos.y, vOriPos.z);
	}
}
