using UnityEngine;

public class OptionsController : UIController<OptionsView> {
	protected override void Detach(OptionsView view) {
		if(view == null) return;

		view.LanguageSelection.onValueChanged.RemoveListener(this.OnLanguageSelectionChanged);
	}

	protected override void Attach(OptionsView view) {
		if(view == null) return;

		view.LanguageSelection.onValueChanged.AddListener(this.OnLanguageSelectionChanged);

		view.LanguageSelection.options.Clear();

		for(int i = 0; i < Localization.Supported.Length; ++i) {
			view.LanguageSelection.options.Add(new UnityEngine.UI.Dropdown.OptionData(Localization.Supported[i]));
		}

		var index = System.Array.FindIndex(Localization.Supported, i => string.Compare(i, DataManager.GetAppData().Lang) == 0);

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