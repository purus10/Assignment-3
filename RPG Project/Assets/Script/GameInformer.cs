using UnityEngine;
using System.Collections;

public class GameInformer : MonoBehaviour {

	static public int money, idler;
	static public KeyCode Up,Down,Left,Right,GoddessTog,ItemTog,Select,Fight,Special,Deselect;
	static public KeyCode[] A = new KeyCode[4];
	static public bool stop, battle, ambushed;
	static public Transform target;
	public GameObject[] Character;
	public Vector3[] position;
	public Quaternion[] Rotation;
	static public string previous;
	
	void Awake()
	{
		UnityEngine.Cursor.visible = false;
		previous = Application.loadedLevelName;
		if (target == null) CreateCharacter();
		Up = KeyCode.W;
		Down = KeyCode.S;
		Left = KeyCode.A;
		Right = KeyCode.D;
		A[0] = KeyCode.W;
		A[1] = KeyCode.A;
		A[2] = KeyCode.S;
		A[3] = KeyCode.D;
		Select = KeyCode.Mouse0;
		GoddessTog = KeyCode.LeftShift;
		ItemTog = KeyCode.E;
		Deselect = KeyCode.Escape;
		Fight = KeyCode.F;
	}
	
	void CreateCharacter()
	{
		for(int i = 0; i < Character.Length;i++)
			Instantiate (Character[i], position[i],Rotation[i]);
	}

	void FixedUpdate()
	{
		if (target == null) target = GameObject.Find(Character[0].name).transform;
	}
}
