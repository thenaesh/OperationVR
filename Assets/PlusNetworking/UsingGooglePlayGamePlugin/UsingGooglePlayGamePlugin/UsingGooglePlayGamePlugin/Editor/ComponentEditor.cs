using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlusNetworkManager))]
public class PlusComponentEditor : Editor
{
	[SerializeField]
	private bool friend;

	void OnEnable()
	{

	}

	public override void OnInspectorGUI()
	{
		PlusNetworkManager _gmCtrl = (PlusNetworkManager)target;
		DrawDefaultInspector();
		GUILayout.Space(10);
		GUILayout.Label ("-------------------Enable Team Match--------------------\n      (Total Should not exceed MAX PLAYER)");
		GUILayout.Space(10);
		GUILayout.BeginHorizontal();
		GUILayout.Label("Team_Match", GUILayout.Width(122));
		_gmCtrl.teamMatch = EditorGUILayout.Toggle(_gmCtrl.teamMatch);
		GUILayout.EndHorizontal();
		if (_gmCtrl.teamMatch)
		{
			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.Label("No of Friend", GUILayout.Width(122));
			_gmCtrl.noOfFriend = EditorGUILayout.IntField(_gmCtrl.noOfFriend);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Label("No of Enemy", GUILayout.Width(122));
			_gmCtrl.noOfEnemy = EditorGUILayout.IntField(_gmCtrl.noOfEnemy);
			GUILayout.EndHorizontal();
		}
		int xyz = AddTag("plusFriend");
		xyz = AddTag("plusEnemy");
		xyz = AddTag("plusMyPlayer");
		xyz = AddTag("plusObject");
		xyz = AddTag("plusGameOwner");
		//Debug.Log (xyz);
		//xyz = xyz + 1;
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}

	public static int AddTag(string tag)
	{
		SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
		SerializedProperty tagsProp = tagManager.FindProperty("tags");
		int z = 1;

		SerializedProperty layersProp = tagManager.FindProperty("layers");
		bool found = false;
		for (int i = 0; i < tagsProp.arraySize; i++)
		{
			SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
			if (t.stringValue.Equals(tag)) { found = true; break; }
		}
		if (!found)
		{
			tagsProp.InsertArrayElementAtIndex(0);
			SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
			n.stringValue = tag;
		}
		tagManager.ApplyModifiedProperties();
		return z;
	}
}

