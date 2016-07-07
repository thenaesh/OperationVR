using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlusNetworkTransform))]
public class PlusComponentEditor1 : Editor
{
	[SerializeField]

	private PlusNetworkTransform _gmCtrl = null;

	void OnEnable()
	{
		_gmCtrl = (PlusNetworkTransform)target;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Transform Type", GUILayout.Width(120));
		_gmCtrl.transformationType = (PlusNetworkTransform.tType)EditorGUILayout.EnumPopup(_gmCtrl.transformationType);

		if(_gmCtrl.transformationType == PlusNetworkTransform.tType.rigidbody3D)
		{			
			if(_gmCtrl.GetComponent<Rigidbody>() == null)
			{			
				EditorUtility.DisplayDialog("Plus Networking","Need Rigidbody3D component on this gameObject for Plus Networking Transform", "Okay");
				_gmCtrl.transformationType = PlusNetworkTransform.tType.none;
			}
		}

		if(_gmCtrl.transformationType == PlusNetworkTransform.tType.rigidbody2D)
		{			
			if(_gmCtrl.GetComponent<Rigidbody2D>() == null)
			{			
				EditorUtility.DisplayDialog("Plus Networking","Need Rigidbody2D component on this gameObject for Plus Networking Transform", "Okay");
				_gmCtrl.transformationType = PlusNetworkTransform.tType.none;
			}			
		}

		GUILayout.EndHorizontal();

		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}
}

