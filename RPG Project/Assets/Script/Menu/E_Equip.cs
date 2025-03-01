﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class E_Equip : MonoBehaviour {
	
	public bool gearID,items,weapons,accessories, abilities, blessings;
	public Rect Box, Button;
	public Rect Scroller, ScrollView;
	public int magicnum;
	public int bonus, bonus1, bonus2;
	int compare;
	
	private float vScroolbarValue;
	public Vector2 scrollposition = Vector2.zero;
	
	public List <Item> E_items = new List<Item>();
	public List <Weapon> E_weapons = new List<Weapon>();
	public List <Ability> E_abilities = new List<Ability>();
	public List <Accessory> E_acc = new List<Accessory>();
	
	E_Status selected;
	E_Info info;
	PC_Main equip;
	
	string maxdisplay(int i)
	{
		if (E_weapons[i].name != "None" && E_weapons[i].type != 4) 
			return "   " + E_weapons[i].max + " :Max ";
		else return "";
	}
	
	void FixedUpdate()
	{
		selected = GetComponent<E_Status>();
		info = GetComponent<E_Info>();
		equip = selected.selected.GetComponent<PC_Main>();
	}
	#region Stat bonus Checkers
	void WepCompare(int d, int h, int i)
	{
		if (E_weapons.Count > 0)
		{
			info.info = "Damage: " + E_weapons[i].damage+"  Hit: " + E_weapons[i].hit;
			compare = (E_weapons[i].damage - d) + d;
			if (compare == d) selected.DMGFont.normal.textColor = Color.white;
			if (compare > d) selected.DMGFont.normal.textColor = Color.green;
			if (compare < d) selected.DMGFont.normal.textColor = Color.red;
			equip.damage = bonus + compare;
			
			compare = (E_weapons[i].hit - h) + h;
			if (compare == h) selected.HITFont.normal.textColor = Color.white;
			if (compare > h) selected.HITFont.normal.textColor = Color.green;
			if (compare < h) selected.HITFont.normal.textColor = Color.red;
			equip.hit = bonus1 + compare;
		}
	}
	
	void AccCompare(int b,int t,int c, int i)
	{
	/*	if (E_acc.Count > 0)
		{
			info.info = "BRW: " + E_acc[i].Brawns +"  TEN: " + E_acc[i].Tenacity +"  CRG: " + E_acc[i].Courage;
			compare = (E_acc[i].Brawns - b) + b;
			if (compare == b) selected.BRWFont.normal.textColor = Color.white;
			if (compare > b) selected.BRWFont.normal.textColor = Color.green;
			if (compare < b) selected.BRWFont.normal.textColor = Color.red;
			equip.stats[0,0] = bonus + compare;
			
			compare = (E_acc[i].Tenacity - t) + t;
			if (compare == t) selected.TENFont.normal.textColor = Color.white;
			if (compare > t) selected.TENFont.normal.textColor = Color.green;
			if (compare < t) selected.TENFont.normal.textColor = Color.red;
			equip.stats[0,1] = bonus1 + compare;
			
			compare = (E_acc[i].Courage - c) + c;
			if (compare == c) selected.CRGFont.normal.textColor = Color.white;
			if (compare > c) selected.CRGFont.normal.textColor = Color.green;
			if (compare < c) selected.CRGFont.normal.textColor = Color.red;
			equip.stats[0,2] = bonus2 + compare;
		}*/
	}
	#endregion
	#region Equipping
	void WepEquip(int i)
	{
		selected.DMGFont.normal.textColor = Color.white;
		selected.HITFont.normal.textColor = Color.white;
		equip.wep = E_weapons[i];
		E_weapons.Clear();
		weapons = false;
		gearID = false;
		//equip.Equip();
	}
	
	void AccEquip(int i)
	{
		selected.BRWFont.normal.textColor = Color.white;
		selected.TENFont.normal.textColor = Color.white;
		selected.CRGFont.normal.textColor = Color.white;
		equip.acc = E_acc[i];
		E_acc.Clear();
		accessories = false;
		gearID = false;
		//equip.Equip();
	}
	
	void ItemEquip(int s,int i)
	{
		equip.items[s] = E_items[i];
		E_items.Clear();
		equip.items[s].equipped = true;
		items = false;
		gearID = false;
	}
	
	void BlessEquip(int s,int i)
	{
		equip.ability[s] = E_abilities[i];
		//	passive.Blessings(equip.ability[s].name);
		E_abilities.Clear();
		equip.ability[s].equipped = true;
		blessings = false;
		gearID = false;
		//equip.Equip();
	}
	
	void AbilityEquip(int s,int i)
	{
		equip.ability[s] = E_abilities[i];
		E_abilities.Clear();
		equip.ability[s].equipped = true;
		abilities = false;
		gearID = false;
	}
	#endregion
	
	
	void GuiSetup(int i)
	{
		Button = new Rect(2.6f,(Button.height * i) + 2,425.3f,40f);
		Button.y = 40.3f * i;
		ScrollView.height = Button.y;
	}
	
	void OnGUI () 
	{	
		scrollposition = GUI.BeginScrollView(Scroller,scrollposition,ScrollView);
		GUI.Box(Box,"");
		
		if (weapons) for (int i = 0; i < E_weapons.Count; i++)
		{
			GuiSetup(i);
			if (GUI.Button(Button, E_weapons[i].name + maxdisplay(i))) WepEquip(i);
			
			if (Button.Contains(Event.current.mousePosition)) 
				WepCompare(equip.wep.damage,equip.wep.hit,i);
		}
		
		if (accessories) for (int i = 0; i < E_acc.Count; i++)
		{
			GuiSetup(i);
			if (GUI.Button(Button, E_acc[i].name)) AccEquip(i);
			
			if (Button.Contains(Event.current.mousePosition)) 
				AccCompare(equip.acc.Brawns,equip.acc.Tenacity,equip.acc.Courage,i);
		}
		
		if (blessings) for (int i = 0; i < E_abilities.Count; i++)
		{
			GuiSetup(i);
			if (GUI.Button(Button, E_abilities[i].name)) BlessEquip(magicnum,i);
		}
		
		if (abilities) for (int i = 0; i < E_abilities.Count; i++)
		{
			GuiSetup(i);
			if (GUI.Button(Button, E_abilities[i].name)) AbilityEquip(magicnum,i);
		}
		
		if (items) for (int i = 0; i < E_items.Count; i++)
		{
			GuiSetup(i);
			if (GUI.Button(Button, E_items[i].name)) ItemEquip(magicnum,i);
		}
		GUI.EndScrollView();
	}
}

