using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MazeMaker))]
public class MazeMakerEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		EditorGUILayout.BeginHorizontal();

		if(GUILayout.Button("Generate")) {
			var target = this.target as MazeMaker;

			target.Generate();

			this.serializedObject.ApplyModifiedProperties();

			EditorUtility.SetDirty(target);
		}

		if(GUILayout.Button("Regenerate")) {
			var target = this.target as MazeMaker;

			target.Regenerate();

			this.serializedObject.ApplyModifiedProperties();

			EditorUtility.SetDirty(target);
		}

		EditorGUILayout.EndHorizontal();
	}
}