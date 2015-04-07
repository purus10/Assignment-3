using UnityEngine;
using System.Collections;

public class Ticket_Dispenser : MonoBehaviour {

	static public bool ticket = false;
	static public int nowserving_number;
	public int Default_min, Default_max,NowServing_Timer, Battle_Increase, Walk_Increase, Talk_Increase;
	public TextMesh Default_Display;
	int t,m;
	int default_number()
	{
		return PC_Main.Ticket + Random.Range(Default_min,Default_max);
	}
	int ticket_number()
	{
		return Random.Range(nowserving_number,Default_max);
	}

	void FixedUpdate()
	{
		if (nowserving_number != PC_Main.Ticket) t++;
		DisplayServing();
	}
	void OnTriggerStay()
	{
		if (ticket == false)
			if (Input.GetKeyDown(GameInformer.Select)) 
		{
			PC_Main.Ticket = ticket_number();
			ticket = true;
			Debug.Log (PC_Main.Ticket);
		}
	}
	void DisplayServing()
	{
		if (nowserving_number == 0) nowserving_number = default_number();
		if (t >= NowServing_Timer) 
		{
			nowserving_number++;
			t = 0;
		}
		Default_Display.text = nowserving_number.ToString();
		if (nowserving_number == PC_Main.Ticket) Beep ();
	}
	void Beep()
	{
		Debug.Log("IT IS YOUR TURN PLEASE RETURN ASAP FOR TREATMENT");
	}
}
