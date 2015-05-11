using UnityEngine;
using System.Collections;

public class Froge_AI : MonoBehaviour {

	public PC_AI AI;
	
	void FixedUpdate () 
	{
		if (AI.turn != 0)
		{
			if(AI.turn == 1) print ("Moo");
			if (AI.turn > 1 && AI.turn < 5)
			{
				PC_Animator.F_attack = true;
				AI.main.Target_type = AI.main.ability[AI.turn-2].type;
				AI.main.target = null;
				AI.main.Target();
				AI.main.CastAbility(AI.main.ability[AI.turn-2]);
			}
		 //Add functions here
			AI.turn = 0;
		}
	}
}
