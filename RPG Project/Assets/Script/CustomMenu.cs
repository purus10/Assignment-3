/*using UnityEngine;
using UnityEditor;
using System.Collections;

public class CustomMenu : MonoBehaviour {
	
	[CustomEditor(typeof(PC_Main))]
	public class ListTesterInspector : Editor 
	{
		public override void OnInspectorGUI () 
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("Name"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("ID"));
			//EditorList.Show(serializedObject.FindProperty("Stats"));
			serializedObject.ApplyModifiedProperties();
		}
	}
	
	/*public static class EditorList {
		
		public static void Show (SerializedProperty list) {
			{
				EditorGUILayout.PropertyField(list);
				EditorGUI.indentLevel += 1;
				if (list.isExpanded) {
					for (int i = 0; i < list.arraySize; i++) {
						list.GetArrayElementAtIndex(i).stringValue =  "Moop";
						EditorGUILayout.PropertyField (list.GetArrayElementAtIndex (i));
					}
				}
				EditorGUI.indentLevel -= 1;
			}
		}
	}*/
//}
