using UnityEngine;

public class OptionsController : UIController<OptionsView> {
	protected override void Detach(OptionsView view) {
		if(view == null) return;

		view.LanguageSelection.onValueChanged.RemoveListener(this.OnLanguageSelectionChanged);
	}

	protected override void Attach(OptionsView view) {
		if(view == null) return;

		view.LanguageSelection.onValueChanged.AddListener(this.OnLanguageSelectionChanged);

		var index = System.Array.FindIndex(this.View.LanguageSelection.options.ToArray(), i => string.Compare(i.text, DataManager.GetAppData().Lang) == 0);

		if(index == -1) index = 0;

		this.View.LanguageSelection.value = index;
	}

	protected virtual void OnLangChanged(string value) {
		if(this.View == null) return;

		this.View.Lang = value;
	}

	protected virtual void OnLanguageSelectionChanged(int index) {
		var lang = View.LanguageSelection.options[index].text;

		DataManager.GetAppData().Lang = lang;
	}
}