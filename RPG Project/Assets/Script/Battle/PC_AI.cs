using UnityEngine;
using System.Collections;

public class PC_AI : MonoBehaviour {

	public PC_Main main;
	public int Heal_At_Percent, turn = 0;
	float[] Cooldowns = new float[4];
	int Heal_Percent
	{
		get {return main.cur_hp*Heal_At_Percent/100;}
	}
	void Cooldown()
	{
		for (int i = 0; i < main.ability.Length;i++)
		Cooldowns[i] = main.ability[i].cooldown;
	}
	int SetTurn(int HP)
	{
		int t = 0;
		if ( HP <= Heal_Percent) t = 1;
		else if (Cooldowns[0] == 0f) t = 2;
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
