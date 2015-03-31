using UnityEngine;
using System.Collections;
using Database;

namespace Database
{
	#region Ability
	public class Ability
	{ 
		public string name, description;
		public bool equipped, far_range;
		public float max_range, min_range;
		public int type,ID;
		int dmg;
		
		bool FarRangeHit(float d, PC_Main my, NPC_Main t)
		{
			int my_hit = Random.Range (0,100);
			int t_dodge = Random.Range (0,100);
			if (d < min_range && d > max_range) return my_hit > t_dodge;
			else return false;
		}
		
		bool CloseRangeHit(PC_Main my, NPC_Main t)
		{
			int my_hit = Random.Range (0,100);
			int t_dodge = Random.Range (0,100);
			return my_hit > t_dodge;
		}
		
		bool CritChance(PC_Main my, NPC_Main t)
		{
			int my_hit = Random.Range (0,100);
			int t_dodge = Random.Range (0,100);
			return my_hit > t_dodge;
		}
		
		public void AllyCast(PC_Main my, PC_Main t)
		{
			float distance = Vector3.Distance(my.transform.position, t.transform.position);
			if (distance <= max_range) Execute(my,t,null);
			my.EndTurn();
		}
		
		public void OpposeCast(PC_Main my, NPC_Main t)
		{
			if (far_range == true) 
			{
				float distance = Vector3.Distance(my.transform.position, t.transform.position);
				if (FarRangeHit(distance,my,t) == true) Execute(my,null,t);
				else HUD.info = "MISS!!!";
			}else{
				if (CloseRangeHit(my,t) == true) Execute(my,null,t);
				else HUD.info = "MISS!!!";
			}
			my.EndTurn();
		}
		
		public void Execute(PC_Main my, PC_Main a, NPC_Main t)
		{
			if (name == "Attack") Attack(my,t);
		}
		
		public void Attack(PC_Main my, NPC_Main t)
		{
			dmg = (my.damage * (int) Random.Range(1,1.125f));
			if (dmg > 0) 
			{
				t.cur_hp -= dmg;
				HUD.info = t.Name + " Remaining HP: "+t.cur_hp;
			}
		}
	}
	#endregion
	#region Item
	public class Item
	{
		public string name,a_name;
		public int type;
		public int heal;
		public int amount;
		public bool equipped;
		
		public void CastItem(int j, Item i, PC_Main my, PC_Main t)
		{
			if ( i.type == 0)
			{
				if (i.heal + t.cur_hp <= t.HP) t.cur_hp += i.heal;
				else t.cur_hp = t.HP;
				i.amount--;
				for (int k = 0; k < my.items.Length;k++)
					if ( my.items[k] != null)
						if ( my.items[k].amount == 0) my.items[k] = null;
				HUD.info = t.name+ " heals "+i.heal+" HP!";
			}
		}
	}
	#endregion
	#region Equip
	public class Weapon
	{
		public string name;
		public string description;
		public bool equipped;
		
		public Item[] metal = new Item[3];
		
		public int hit;
		public int damage;
		public int max;
		public int weight;
		public int price;
		public int type;
	}
	
	public class Accessory
	{
		public string name;
		public string description;
		public bool equipped;
		
		public Item[] metal = new Item[3];
		
		public int Damage;
		public int Hit;
		public int Brawns;
		public int Tenacity;
		public int Courage;
		public int price;
		public int type;
	}
	#endregion
}