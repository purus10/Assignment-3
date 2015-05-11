using UnityEngine;
using System.Collections;

public class Treatment : MonoBehaviour {
	
	PC_Main p;
	string saying;
	public Rect dialog_box;
	int choice;

	void OnTriggerEnter (Collider col) 
	{
		p = col.GetComponent<PC_Main>();
	}
	
	void OnTriggerStay()
	{
		if (p != null)
		{
			if (PC_Main.Ticket == Ticket_Dispenser.nowserving_number)
			{
				Ticket_Dispenser.ticket = false;
				saying ="heal!!!";
				PC_Main[] search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
				foreach(PC_Main n in search) n.cur_hp = n.HP;
			} else if (PC_Main.Ticket > Ticket_Dispenser.nowserving_number)  saying = "please wait your turn";
			else if (PC_Main.Ticket < Ticket_Dispenser.nowserving_number)  saying = "Im sorry your ticket expired";
		}
		choice = 1;
	}

	void OnGUI()
	{
		if (choice == 1) GUI.Box(dialog_box,saying);
	}

	void OnTriggerExit()
	{
		p = null;
		if (choice != 0) choice = 0;
	}
}
