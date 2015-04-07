using UnityEngine;
using System.Collections;

public class NPC_Regular : MonoBehaviour {

	public Rect dialog_box, selection_box, acc_button, wep_button, quit_button;
	public string comment;
	public bool Choices;
	int choice = 0;
	public GameObject menu;
	PC_Main p;
	
	void OnTriggerEnter (Collider col) 
	{
		p = col.GetComponent<PC_Main>();
		if (p != null)
			if (choice == 0) choice = 1;
	}
	
	void OnTriggerStay()
	{
		if (choice == 0 && p != null)
			if (Input.GetKeyDown(GameInformer.Select)) choice = 1;
	}
	
	void OnTriggerExit()
	{
		if (choice != 0) choice = 0;
	}
	
	void OnGUI() 
	{
		if (choice == 1)
		{
			GUI.Box(dialog_box,comment);
			if (Input.GetKeyDown(GameInformer.Select) && Choices == true) choice = 2;
			else if (Input.GetKeyDown(GameInformer.Select)) choice = 0;
		}
		if (choice == 2)
		{
			UnityEngine.Cursor.visible = true;
			GUI.Box(selection_box,"What will it be?");
			
			if (GUI.Button(quit_button,"Done for now"))
			{
				choice = 0;
				UnityEngine.Cursor.visible = false;
			}
		}
	}
}
