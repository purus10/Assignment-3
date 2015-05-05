using UnityEngine;
using System.Collections;

public class PC_Animator : MonoBehaviour {

	static public bool attack;
	static public bool move;
	public float attack_wait;
	float t = 0;
	public Animator animator;
	
	void FixedUpdate () 
	{
		if (GameInformer.stop == false)
		{
			if (Input.GetKey(GameInformer.Left) || Input.GetKey(GameInformer.Right) || Input.GetKey(GameInformer.Up) || Input.GetKey(GameInformer.Down)) animator.SetFloat("Speed", 5);
			else animator.SetFloat("Speed", 0);
		} else {
		animator.SetBool("Battle",GameInformer.battle);
			animator.SetBool("Move",move);
			if (attack == true)
			{
				t++;
				if ( t >= attack_wait)
				{
					print ("yes");
					attack = false;
				}
			}
			animator.SetBool("Attack",attack);
		}
	}
}
