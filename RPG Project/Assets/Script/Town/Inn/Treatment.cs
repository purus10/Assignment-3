using UnityEngine;
using System.Collections;

public class Treatment : MonoBehaviour {
	
	PC_Main p;
	
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
				print ("heal!!!");
				PC_Main[] search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
				foreach(PC_Main n in search) n.cur_hp = n.HP;
			} else if (PC_Main.Ticket > Ticket_Dispenser.nowserving_number)  print ("please wait your turn");
			else if (PC_Main.Ticket < Ticket_Dispenser.nowserving_number)  print ("Im sorry your ticket expired");
		}
	}

	void OnTriggerExit()
	{
		p = null;
	}
}
