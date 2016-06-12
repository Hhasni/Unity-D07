using UnityEngine;
using System.Collections;

public class CrosshairScript : MonoBehaviour {
	
	public Texture2D		Cross;
	public Rect				Pos;
	static bool	OriginalOn = true;
	// Use this for initialization
	void Start () {
	
		Pos = new Rect ((Screen.width - Cross.width) / 2, (Screen.height - Cross.height) / 2,
		               Cross.width, Cross.height);
	}

//	void OnGui(){
	//	Screen.lockCursor = true;
	//	Screen.showCursor = false;

	//	if (OriginalOn == true)
	//		GUI.DrawTexture (position, Cross);
	//}

	// Update is called once per frame
	void Update () {
	
	}
}
