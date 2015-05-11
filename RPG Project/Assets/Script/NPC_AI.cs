using UnityEngine;
using System.Collections;

public class NPC_AI : MonoBehaviour {

	public NPC_Main main;
	public int Heal_At_Percent, turn;
	float[] Cooldowns = new float[4];
	int Heal_Percent
	{
		get {return (main.cur_hp*Heal_At_Percent)/100;}
	}
	int SetTurn(int HP)
	{
		int t = 0;
		if (HP == 5) t = 1;
		else t = 2;
		return t;
	}
	
	void FixedUpdate () 
	{
		if (main.Bar_fill == main.waitbar)
		{
			turn = SetTurn(main.cur_hp);
			main.Bar_fill = 0;
		}
	}
}
