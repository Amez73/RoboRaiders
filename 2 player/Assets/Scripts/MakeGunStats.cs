using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeGunStat 
{
	[MenuItem("Assets/Create/Gun Stats")]
	public static void Create()
	{
		GunStats asset = ScriptableObject.CreateInstance<GunStats> ();
		AssetDatabase.CreateAsset (asset, "Assets/Prefab/Guns/Gun Stats/GunStats.asset");
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}

}