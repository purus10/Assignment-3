using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PC_Main))]
[CanEditMultipleObjects]
public class CustomMenu : Editor {
	PC_Main mypcmain;
	Rect stat;
	string WepName()
	{
		if (mypcmain.wep != null) return mypcmain.wep.name;
		else return "None";
	}
	string ArmName()
	{
		if (mypcmain.arm != null) return mypcmain.arm.name;
		else return "None";
	}
	string AccName()
	{
		if (mypcmain.acc != null) return mypcmain.acc.name;
		else return "None";
	}
	string AbName(int i)
	{
		if (mypcmain.ability[i] != null) return mypcmain.ability[i].name;
		else return "None";
	}
	string ItName(int i)
	{
		if (mypcmain.items[i] != null) return mypcmain.items[i].name;
		else return "None";
	}
	int WepDamage()
	{
		if (mypcmain.wep != null) return mypcmain.wep.damage;
		else return 0;
	}
	int WepHit()
	{
		if (mypcmain.wep != null) return mypcmain.wep.hit;
		else return 0;
	}
	void OnEnable()
	{
		mypcmain = (PC_Main) target;
	}
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		EditorGUILayout.LabelField("Name", mypcmain.Name);
		EditorGUILayout.LabelField("Level", mypcmain.Level.ToString());
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("HP",mypcmain.cur_hp.ToString()+"     /");
		mypcmain.HP = EditorGUILayout.IntField(mypcmain.HP);
		mypcmain.cur_hp = mypcmain.HP;
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Wait Bar",mypcmain.Bar_fill.ToString()+"     /");
		EditorGUILayout.LabelField(mypcmain.waitbar.ToString());
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Dmg",mypcmain.damage.ToString());
		mypcmain.damage = WepDamage() + mypcmain.Str + mypcmain.Luk/4;
		EditorGUILayout.LabelField("Hit",mypcmain.hit.ToString());
		mypcmain.hit = WepHit() + mypcmain.Dex + mypcmain.Luk/4;
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		mypcmain.Str = EditorGUILayout.IntField("Str",mypcmain.Str);
		mypcmain.Dex = EditorGUILayout.IntField("Dex",mypcmain.Dex);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		mypcmain.Int = EditorGUILayout.IntField("Int",mypcmain.Int);
		mypcmain.Agi = EditorGUILayout.IntField("Agi",mypcmain.Agi);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		mypcmain.Luk = EditorGUILayout.IntField("Luk",mypcmain.Luk);
		mypcmain.Exp = EditorGUILayout.IntField("Exp", mypcmain.Exp);
		EditorGUILayout.EndHorizontal();
		if (PC_Main.Bar_max > 0) mypcmain.Bar_Set = PC_Main.Bar_max;
		mypcmain.Bar_Set = EditorGUILayout.FloatField("Bar Maximum",mypcmain.Bar_Set);
		PC_Main.Bar_max = mypcmain.Bar_Set;
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Weapon: ",WepName());
		EditorGUILayout.LabelField("Armor: ",ArmName());
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.LabelField("Accessory: ",AccName());
		EditorGUILayout.LabelField("");
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Ability 1:                  Attack");
		EditorGUILayout.LabelField("Ability 2: ", AbName(1));
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Ability 3: ", AbName(2));
		EditorGUILayout.LabelField("Ability 4: ", AbName(3));
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.LabelField("");
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Item 1: ", ItName(0));
		EditorGUILayout.LabelField("Item 2: ", ItName(1));
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Item 3: ", ItName(2));
		EditorGUILayout.LabelField("Item 4: ", ItName(3));
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.LabelField("");
		if (mypcmain.ID == 1) EditorGUILayout.LabelField("Mask: ", ItName(2));




	}
}
