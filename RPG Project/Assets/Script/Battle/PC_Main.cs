using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class PC_Main : MonoBehaviour {

	//Turn switch with bar
	//attack cooldown
	//one weapon, one armor, one accessory
	//number lerps for damage
	public static float Bar_max;
	public static int Ticket;
	public static bool sent;
	public string Name;
	public string[] FrogeNames;
	public int ID, HP, Str, Dex, Int, Agi, Luk, Exp, Target_type, cur_hp, damage, hit, index;
	public int[] Stat;//Stats: 0 = Str, 1 = Dex, 2 = Int, 3 = Agi, 4 = Luck
	public float Speed, Rotation, Bar_fill,Bar_Set;
	public bool Myturn;
	public Transform target;
	public List <Transform> targets = new List<Transform>();
	public List <Ability> abilities = new List<Ability>();
	public Weapon wep;
	public Armor arm;
	public Accessory acc;
	public Ability[] ability = new Ability[4];
	public Item[] items = new Item[4];
	public NavMeshAgent Agent;
	public NPC_Main NPC;
	public PC_Main PC;
	Color target_off;
	int t;
	public int Level
	{
		get {return Exp/200;}
	}
	public float waitbar
	{
		get {return Bar_max-Agi;}
	}
	void Start ()
	{
		Ability a = new Ability();
		a.name = "Attack";
		a.type = 1;
		abilities.Add(a);
		DontDestroyOnLoad(gameObject);
		SetStats();
		//FirstWeapon();
		target_off = GetComponent<SpriteRenderer>().color;
	}
	void Update()
	{
		if (sent == false) 
		{
			transform.position = MapTransition.placement;
			sent = true;
		}
		if (Ticket != 0)
		{
			t++;
			if ( t > 30 && Application.loadedLevelName != "Inn Test")
			{
				Ticket_Dispenser.nowserving_number++;
				t = 0;
			}
			if (ID == 1 && Ticket_Dispenser.nowserving_number == Ticket) Ticket_Dispenser.Beep (this);
		}
		SetAttack();
		NPCMotions();
		transform.rotation = new Quaternion(0,transform.rotation.y,0,0);
		if (GameInformer.battle == false)
		{
		if (Application.loadedLevelName == "Battle") BattleSetup();
		}else BarFill();
		TargetSetup();
		CharacterMotion();
		if (Input.GetKeyDown(KeyCode.Space)) EndTurn();
	}
	void CharacterMotion()
	{
		if (GameInformer.stop == false)
		{
			if (Input.GetKey(GameInformer.Left)) transform.rotation = new Quaternion(transform.rotation.x,180,transform.rotation.z,0);
			if (Input.GetKey(GameInformer.Right)) transform.rotation = new Quaternion(transform.rotation.x,0,transform.rotation.z,0);
			if (GameInformer.target == transform)
			{
				if (Application.loadedLevelName != "Battle")
				{
				Agent.Stop();
					if (Input.GetKey(GameInformer.Up))
					{
						if (transform.rotation.y > 0) transform.Translate(-Vector3.forward * Speed * Time.deltaTime);
						else transform.Translate(Vector3.forward * Speed * Time.deltaTime);
					}

				if (Input.GetKey(GameInformer.Left)) transform.Translate(-Vector3.left * Speed * Time.deltaTime);
				if (Input.GetKey(GameInformer.Right)) transform.Translate(-Vector3.left * Speed * Time.deltaTime);
				if (Input.GetKey(GameInformer.Down))
					{
						if (transform.rotation.y > 0) transform.Translate(Vector3.forward * Speed * Time.deltaTime);
						else transform.Translate(-Vector3.forward * Speed * Time.deltaTime);
					}
				} else {
					Agent.Stop();
					if (Input.GetKey(GameInformer.Up)) transform.Translate(Vector3.forward * Speed * Time.deltaTime);
					if (Input.GetKey(GameInformer.Left)) transform.Rotate(Vector3.down * Speed * 34 * Time.deltaTime);
					if (Input.GetKey(GameInformer.Right)) transform.Rotate(Vector3.up * Speed * 34 * Time.deltaTime);
					if (Input.GetKey(GameInformer.Down)) transform.Translate(-Vector3.forward * Speed * Time.deltaTime);
				}
			}
		}else if(Myturn == true && GameInformer.target == transform)
		{
			for(int i = 0; i < GameInformer.A.Length;i++)
			{
				if (Input.GetKey(GameInformer.ItemTog) && Input.GetKeyDown(GameInformer.A[i])) 
					if (items[i] != null) CastItem(i,items[i]);		
				if (!Input.GetKey(GameInformer.ItemTog) && Input.GetKeyDown(GameInformer.A[i])) 
					if (ability[i] != null) CastAbility(ability[i]);
			}

		}
	}
	void BarFill()
	{
		if (Myturn == false && Bar_fill != waitbar) Bar_fill++;
		if (Bar_fill == waitbar) Myturn = true;
	}
	void TargetSetup()
	{
		if (Input.GetKeyDown(KeyCode.Tab) && Target_type > 0) 
		{
			if (Myturn == true)
			{
				targets.Clear();
				Target();
			}
		}
	}
	public void BattleSetup()
	{
		Agent.Resume();
		if (GameInformer.target == transform && GameInformer.stop == false) 
		{
			GameInformer.stop = true;
			GameInformer.battle = true;
		}
	}
	void SetAttack()
	{
		foreach (Ability a in abilities) 
			if (a.name == "Attack") 
		{
			a.equipped = true;
			ability[0] = a;
		}
	}
	
	void NPCMotions()
	{
		if (GameInformer.target != null && GameInformer.stop == false && GameInformer.target != transform) 
		{
			Agent.Resume();
			Agent.stoppingDistance = 1.5f;
			Agent.SetDestination(GameInformer.target.position);
		}
	}

	public void CastAbility(Ability a)
	{
		Agent.stoppingDistance = 1.5f;
		if (ID == 0)
		{
		foreach (Transform tar in targets)
		{
				if (tar.GetComponent<PC_Main>() != null) tar.GetComponent<SpriteRenderer>().color = tar.GetComponent<PC_Main>().target_off;
				if (tar.GetComponent<NPC_Main>() != null) tar.GetComponent<SpriteRenderer>().color = tar.GetComponent<NPC_Main>().target_off;
		}
		}
		if (Target_type != a.type) 
		{
			targets.Clear();
			target = null;
			Target_type = a.type;
			if (Target_type != 3) Target();
			else {
				if (target != null) NPC = target.GetComponent<NPC_Main>();
				a.Execute(this,null,NPC);
			}
		} else {
			if (Target_type == 1) 
			{
				float distance = Vector3.Distance(transform.position, target.position);
				if (a.far_range == false && distance > 2f) 
				{
					Agent.SetDestination(target.position);
					NPC = target.GetComponent<NPC_Main>();
					StartCoroutine(CloseGap(a));
				} else if (distance <= 2f) 
				{
					a.OpposeCast(this,NPC);
				}
			}
			if (Target_type == 2) 
			{
				PC = target.GetComponent<PC_Main>();
				a.AllyCast(this,PC);
			}
		}
	}
	
	IEnumerator CloseGap(Ability a)
	{
		while (Vector3.Distance(transform.position, target.position) > Agent.stoppingDistance - 0.5f)
		{
			if (Vector3.Distance(transform.position, target.position) <= Agent.stoppingDistance + 0.5f)
			{
				if (ID == 1) PC_Animator.move = true;
				a.OpposeCast(this,NPC);
				break;
			}
			yield return null;
		}
	}
	
	void CastItem(int j, Item i)
	{
		if (Target_type != 2) 
		{
			Target_type = 2;
			targets.Clear();
			Target();
		} else {
			PC = target.GetComponent<PC_Main>();
			i.CastItem(j,i,this,PC);
		}	
	}
	
	public void EndTurn()
	{
		Myturn = false;
		Bar_fill = 0;
		GameInformer.idler = (GameInformer.idler +1) % 8;
		if (ID == 0)
		{
		foreach (Transform n in targets)
		{
				if (n.GetComponent<PC_Main>() != null) n.GetComponent<SpriteRenderer>().color = n.GetComponent<PC_Main>().target_off;
				if (n.GetComponent<NPC_Main>() != null) n.GetComponent<SpriteRenderer>().color = n.GetComponent<NPC_Main>().target_off;
		}
		}
	}
	void SetStats()
	{
		name = Name;
		cur_hp = HP;
		Stat[0] = Str;
		Stat[1] = Dex;
		Stat[2] = Int;
		Stat[3] = Agi;
		Stat[4] = Luk;
		Bar_max = Bar_Set;
	}
	
	/*void FirstWeapon()
	{
		if (wep[0] == null) foreach (weapon weapon in WeaponsList.weapons)
		{
			if (ID == weapon.type)
			{
				wep[0] = weapon;
				Equip ();
			}
		}
	}*/
	
	/*public void Equip()
	{
		int d = Brawns, h = Tenacity,b = Brawns, t = Tenacity, c = Courage;
		for (int i = 0; i < wep.Length;i++)
		{
			if (wep[i] != null)
			{
				wep[i].equipped = true;
				d += wep[i].damage;
				h += wep[i].hit;
			}
		}
		for (int i = 0; i < acc.Length;i++)
		{
			if (acc[i] != null)
			{
				acc[i].equipped = true;
				b += acc[i].Brawns;
				t += acc[i].Tenacity;
				c += acc[i].Courage;
			}
		}
		
		damage = d;
		hit = h;
		for(int i = 0; i < stats.GetLength(0);i++)
		{
			stats[i,0] = b;
			stats[i,1] = t;
			stats[i,2] = c;
		}
	}*/
	
	public void Target()
	{
		targets.Clear();
		if (Target_type == 1) 
		{
			NPC_Main[] search = GameObject.FindObjectsOfType(typeof(NPC_Main)) as NPC_Main[];
			foreach (NPC_Main n in search) targets.Add(n.transform);
		} else if (Target_type == 2)
		{
			PC_Main[] search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
			foreach (PC_Main n in search) targets.Add(n.transform);
		} else if (Target_type == 3) target = transform;
		if (target == null)
		{
			targets.Sort(delegate(Transform t1, Transform t2) { 
				return (Vector3.Distance(t1.position, transform.position)).CompareTo(Vector3.Distance(t2.position,transform.position));});
			if (Target_type != 2 )target = targets[0];
			else {
				target = transform;
			}
		} else if (targets.Count > 1) 
		{
			index = (index+1) % targets.Count;
			target = targets[index];
			if (Target_type != 0)  transform.LookAt(target);
		}
		if (ID ==1)
		{
		foreach (Transform tar in targets) if (tar == target)
				tar.GetComponent<SpriteRenderer>().color = Color.blue;
		else {
				if (tar.GetComponent<PC_Main>() != null) tar.GetComponent<SpriteRenderer>().color = tar.GetComponent<PC_Main>().target_off;
				if (tar.GetComponent<NPC_Main>() != null) tar.GetComponent<SpriteRenderer>().color = tar.GetComponent<NPC_Main>().target_off;
		}
		}
	}
}
