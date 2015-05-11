using UnityEngine;
using System.Collections;

public class Ticket_Dispenser : MonoBehaviour {

	static public bool ticket = false;
	static public int nowserving_number;
	public int Default_min, Default_max,NowServing_Timer, Battle_Increase, Walk_Increase, Talk_Increase;
	public Rect dialog_box;
	public TextMesh Default_Display;
	static public AudioClip timer;
	public AudioClip audio;
	int t,m, choice;
	int default_number()
	{
		return PC_Main.Ticket + Random.Range(Default_min,Default_max);
	}
	int ticket_number()
	{
		return Random.Range(nowserving_number,Default_max);
	}

	void Start()
	{
		timer = audio;
	}

	void FixedUpdate()
	{
		if (nowserving_number != PC_Main.Ticket) t++;
		DisplayServing();
	}
	void OnTriggerStay()
	{
		if (ticket == false)
		{
			PC_Main.Ticket = ticket_number();
			ticket = true;
			choice = 1;
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
	}
	static public void Beep(PC_Main p)
	{
		p.GetComponent<AudioSource>().PlayOneShot(timer);
	}

	void OnGUI()
	{
		if (choice == 1) GUI.Box(dialog_box,"Your ticket is "+PC_Main.Ticket.ToString());
	}
	
	void OnTriggerExit()
	{
		if (choice != 0) choice = 0;
	}
}
