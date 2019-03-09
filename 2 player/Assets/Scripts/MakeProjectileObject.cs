using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeProjectileObject 
{
	[MenuItem("Assets/Create/Projectile Object")]
	public static void Create()
	{
		ProjectileObject asset = ScriptableObject.CreateInstance<ProjectileObject> ();
		AssetDatabase.CreateAsset (asset, "Assets/Projectiles/NewProjectileObject.asset");
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}

}