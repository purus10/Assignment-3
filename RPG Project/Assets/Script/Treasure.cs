using UnityEngine;
using System.Collections;
using Database;

public class Treasure : MonoBehaviour {

	public Rect dialog_box;
	int choice = 0;
	public float Display_Time;
	Item[] loot;
	string comment;
	float t;
	PC_Main p;

	void Start()
	{
		loot = new Item[4];
		for (int i = 0; i < loot.Length;i++) loot[i] = new Item();
		loot[0].name = "Potion";
		loot[1].name = "High Potion";
		loot[2].name = "Roasted Chicken";
		loot[3].name = "Bengay";
	}
	
	void OnTriggerEnter (Collider col) 
	{
		p = col.GetComponent<PC_Main>();
		if (p != null && p.items[0] == null)
			if (choice == 0) choice = 1;
	}
	
	void Update()
	{
		if (choice == 1) t++;
		if (t > Display_Time) choice = 0;
	}
	
	void OnGUI() 
	{
		if (choice == 1)
		{
			for (int i = 0 ; i < loot.Length;i++)
			{
				p.items[i] = loot[i];
				dialog_box = new Rect(2.6f,(dialog_box.height * i) + 2,150f,20f);
				comment = "Gained " + loot[i].name;
				GUI.Box(dialog_box,comment);
			}
			if (Input.GetKeyDown(GameInformer.Select)) choice = 0;
		}
		
	}
}
