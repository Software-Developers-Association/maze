using UnityEngine;

public class MainMenuDriver : MonoBehaviour {
	[SerializeField]
	private MainMenuController controller;
	[SerializeField]
	private MainMenuView view;
	[SerializeField, Lang]
	private string lang = "en";

	private void Start() {
		this.controller.View = this.view;

		//var culture = System.Globalization.CultureInfo.CurrentCulture;

		//this.controller.View.Lang = culture.TwoLetterISOLanguageName;
	}

	protected virtual void OnValidate() {
		if(this.controller && this.controller.View) this.controller.View.Lang = this.lang;
	}
}