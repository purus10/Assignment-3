using UnityEngine;
using System.Collections;

public class MapTransition : MonoBehaviour {

	static public Vector3 placement;
	public Vector3 Trans_Place;
	public string Level_Name;

	void OnTriggerEnter(Collider col)
	{
		PC_Main p = col.GetComponent<PC_Main>();

		if (p != null) {
			placement = Trans_Place;
			PC_Main.sent = false;
			if (placement == Trans_Place) Application.LoadLevel(Level_Name);
		}
	}


}
