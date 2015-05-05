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
		public float max_range, min_range,cooldown;
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
			if (my.ID == 1) PC_Animator.attack = true;
			if (my.ID == 1) PC_Animator.move = false;
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

		void Stick (PC_Main my, NPC_Main t, int str, int luk)
		{
			dmg = (my.damage +(str+2) + (luk+1) * (int) Random.Range(1,1.125f));
			if (dmg > 0) 
			{
				t.cur_hp -= dmg;
				Debug.Log( t.Name + " Remaining HP: "+t.cur_hp);
			}
		}
		void GunShotBeak (NPC_Main t, int str, int dex, int luk)
		{
			dmg = ((str+4)+(dex+2)+(luk+2) * (int) Random.Range(1,1.125f));
			if (dmg > 0) 
			{
				t.cur_hp -= dmg;
				Debug.Log( t.Name + " Remaining HP: "+t.cur_hp);
			}
		}
		public void PlaceboCure(PC_Main my, PC_Main t, int luk)
		{
			int heal = (luk+4 * (int) Random.Range(1,1.125f));
			if (heal < t.HP ) t.cur_hp += heal;
			else t.cur_hp = t.HP;
			Debug.Log( t.Name + " Remaining HP: "+t.cur_hp);
		}
		public void FlaskThrow (PC_Main my, PC_Main t, int str, int dex, int luk)
		{
			dmg = ((str+1)+(dex+3)+(luk+2) * (int) Random.Range(1,1.125f));
			if (dmg > 0) 
			{
				t.cur_hp -= dmg;
				//Status Effects;
				Debug.Log( t.Name + " Remaining HP: "+t.cur_hp);
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

	public class Armor
	{
		public string name;
		public string description;
		public bool equipped;
		
		public int Defense;
		public int Hit;
		public int Brawns;
		public int Tenacity;
		public int Courage;
		public int price;
		public int type;
	}
	#endregion
}