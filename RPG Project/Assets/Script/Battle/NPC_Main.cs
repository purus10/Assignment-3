using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class NPC_Main : MonoBehaviour {

	
	public string Name;
	public int HP, Defense, Hit, Beat, Brawns, Tenacity, Courage, Sky_attacks, move_points, tier = 2, Target_type;
	public int[] Tier_Limit = new int[3];
	[HideInInspector] public int cur_hp, cur_beats, index;
	[HideInInspector] public int[] stats = new int[3]; // marks cur (0 = brawns, 1 = tenacity, 2 = courage)
	public float kadabra_timer, speed, tier_count, Bar_fill,waitbar;
	public Transform target;
	public bool myturn, Rose_Innate, kadabra;
	public Item[] items = new Item[4];
	[HideInInspector] public List <Transform> targets = new List<Transform>();
	//[HideInInspector] public float tier_count;
	[HideInInspector] public bool seen = true, cover = false;
	public NavMeshAgent Agent;
	public AudioClip heal;
	Animator animator;
	PC_Main NPC;
	public NPC_AI my;
	
	public Color target_off;
	
	void Awake ()
	{
		SetStats();
		target_off = GetComponent<SpriteRenderer>().color;
		Agent = GetComponent<NavMeshAgent>();
		waitbar = 180;
	}
	
	void Update()
	{
		if (Bar_fill != waitbar && my.turn == 0) Bar_fill++;
		if (cur_hp < 1) Destroy(gameObject);
		transform.rotation = new Quaternion(0,90,0,0);
		if (my.turn == 1) 
		{
			GetComponent<AudioSource>().PlayOneShot(heal);
			cur_hp = 10; 
			HUD.info = "Mushroom just healed itself!";
			my.turn = 0;
		}
		if (my.turn == 2) 
		{
			CastAbility();
		}

	}
	
	void SetStats()
	{
		name = Name;
		cur_hp = HP;
		cur_beats = Beat;
		stats[0] = Brawns;
		stats[1] = Tenacity;
		stats[2] = Courage;
	}

	public void CastAbility()
	{
		Agent.stoppingDistance = 1.5f;
		Target_type = 2;
		Target();
		{
			if (Target_type == 2) 
			{
				float distance = Vector3.Distance(transform.position, target.position);
				if (distance > 2f) 
				{
					Agent.SetDestination(target.position);
					NPC = target.GetComponent<PC_Main>();
					StartCoroutine(CloseGap());
				} else if (distance <= 2f) 
				{
					int chance = Random.Range(1,5);
					if (chance == 5) NPC.cur_hp = NPC.cur_hp - 50;
					my.turn = 0;
				}
			}
		}
	}
	
	IEnumerator CloseGap()
	{
		while (Vector3.Distance(transform.position, target.position) > Agent.stoppingDistance - 0.5f)
		{
			if (Vector3.Distance(transform.position, target.position) <= Agent.stoppingDistance + 0.5f)
			{
				int chance = Random.Range(1,5);
				if (chance == 5) NPC.cur_hp = NPC.cur_hp - 50;
				my.turn = 0;
				break;
			}
			yield return null;
		}
	}
	
	public void Target()
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
			}
		} else if (targets.Count > 1) 
		{
			index = (index+1) % targets.Count;
			target = targets[index];
			if (Target_type != 0)  transform.LookAt(target);
		}
	}
}
