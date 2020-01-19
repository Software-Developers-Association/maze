using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(LangAttribute))]
public class LangAttributeDrawer : PropertyDrawer {
	/// <summary>
	/// <seealso cref="https://en.wikipedia.org/wiki/Language_localisation"/>
	/// </summary>
	static string[] langs = new string[] {
		"ar",
		"bn",
		"zh",
		"nl",
		"en",
		"fr",
		"de",
		"it",
		"pt",
		"es",
		"sv",
		"ru",
		"uk"
	};

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		//base.OnGUI(position, property, label);
		int selected = System.Array.FindIndex(langs, i => string.Compare(i, property.stringValue) == 0);

		if(selected == -1) selected = 0;

		selected = EditorGUI.Popup(position, label.text, selected, langs);

		property.stringValue = langs[selected];
	}
}