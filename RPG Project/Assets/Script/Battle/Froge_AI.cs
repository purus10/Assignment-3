using UnityEngine;
using System.Collections;

public class Froge_AI : MonoBehaviour {

	public PC_AI AI;
	
	void FixedUpdate () 
	{
		if (AI.turn != 0)
		{
			if(AI.turn == 1) print ("Moo");
			if (AI.turn == 2)
			{
				AI.main.Target_type = AI.main.ability[0].type;
				AI.main.Target();
				AI.main.CastAbility(AI.main.ability[0]);
			}
		 //Add functions here
			AI.turn = 0;
		}
	}
}
