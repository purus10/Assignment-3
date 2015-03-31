
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class PC_Main : MonoBehaviour {

	public string Name;
	public int ID, HP, Target_type, Max_equips = 2;
	[HideInInspector] public int cur_hp, damage, hit, index;
	public int[] Stats;
	[HideInInspector] public int[] cur_stats;
	public float Speed, Rotation;
	public bool Myturn;
	public Transform target;
	public List <Transform> targets = new List<Transform>();
	public List <Ability> abilities = new List<Ability>();
	public Weapon[] wep = new Weapon[1];
	public Accessory[] acc;
	public Ability[] ability = new Ability[4];
	public Item[] items = new Item[4];
	public NavMeshAgent Agent;
	public NPC_Main NPC;
	public PC_Main PC;
	Color target_off;
	
	void Start ()
	{
		Ability a = new Ability();
		a.name = "Attack";
		a.type = 1;
		abilities.Add(a);
		acc = new Accessory[Max_equips];
		cur_stats = new int[Stats.Length];
		DontDestroyOnLoad(gameObject);
		SetStats();
		//FirstWeapon();
		target_off = GetComponentInChildren<Renderer>().material.color;
	}
	void Update()
	{
		SetAttack();
		NPCMotions();
		if (Input.GetKeyDown(GameInformer.Fight)) BattleSetup();
		TargetSetup();
		CharacterMotion();
		if (Input.GetKeyDown(KeyCode.Space)) EndTurn();
	}
	void CharacterMotion()
	{
		if (GameInformer.stop == false)
		{
			if (GameInformer.target == transform)
			{
				Agent.Stop();
				if (Input.GetKey(GameInformer.Up)) transform.Translate(Vector3.forward * Speed * Time.deltaTime);
				if (Input.GetKey(GameInformer.Left)) transform.Rotate(Vector3.down * Speed * Rotation * Time.deltaTime);
				if (Input.GetKey(GameInformer.Right)) transform.Rotate(Vector3.up * Speed * Rotation * Time.deltaTime);
				if (Input.GetKey(GameInformer.Down)) transform.Translate(-Vector3.forward * Speed * Time.deltaTime);
			}
		}else if(Myturn == true)
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
	
	void TargetSetup()
	{
		if (Input.GetKeyDown(KeyCode.Tab) && Target_type != 3) 
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
			Myturn = true;
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
		if (GameInformer.target == transform && GameInformer.battle == true) Myturn = true;
	}

	void CastAbility(Ability a)
	{
		Agent.stoppingDistance = 1.5f;
		foreach (Transform tar in targets)
		{
			if (tar.GetComponent<PC_Main>() != null) tar.GetComponentInChildren<Renderer>().material.color = tar.GetComponent<PC_Main>().target_off;
			if (tar.GetComponent<NPC_Main>() != null) tar.GetComponentInChildren<Renderer>().material.color = tar.GetComponent<NPC_Main>().target_off;
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
				} else if (distance <= 2f) a.OpposeCast(this,NPC);
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
		GameInformer.idler = (GameInformer.idler +1) % 8;
		foreach (Transform n in targets)
		{
			if (n.GetComponent<PC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<PC_Main>().target_off;
			if (n.GetComponent<NPC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<NPC_Main>().target_off;
		}
	}
	
	void SetStats()
	{
		name = Name;
		cur_hp = HP;
		for (int i = 0; i < Stats.Length;i++) cur_stats[i] = Stats[i];
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
	
	void Target()
	{
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
				GetComponentInChildren<Renderer>().material.color = Color.blue;
			}
			transform.LookAt(target);
		}
		else if (targets.Count > 1) 
		{
			index = (index+1) % targets.Count;
			target = targets[index];
			if (Target_type != 0)  transform.LookAt(target);
		}
		foreach (Transform tar in targets) if (tar == target)
			tar.GetComponentInChildren<Renderer>().material.color = Color.blue;
		else {
			if (tar.GetComponent<PC_Main>() != null) tar.GetComponentInChildren<Renderer>().material.color = tar.GetComponent<PC_Main>().target_off;
			if (tar.GetComponent<NPC_Main>() != null) tar.GetComponentInChildren<Renderer>().material.color = tar.GetComponent<NPC_Main>().target_off;
		}
	}
}
