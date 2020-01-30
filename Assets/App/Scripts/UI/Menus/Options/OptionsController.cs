using UnityEngine;

public class OptionsController : UIController<OptionsView> {
	protected override void Detach() {
		this.View.LanguageSelection.onValueChanged.RemoveListener(this.OnLanguageSelectionChanged);
	}

	protected override void Attach() {
		this.View.LanguageSelection.onValueChanged.AddListener(this.OnLanguageSelectionChanged);

		this.View.LanguageSelection.options.Clear();

		for(int i = 0; i < Localization.Supported.Length; ++i) {
			this.View.LanguageSelection.options.Add(new UnityEngine.UI.Dropdown.OptionData(Localization.Supported[i]));
		}

		var index = System.Array.FindIndex(Localization.Supported, i => string.Compare(i, DataManager.Instance.AppData.Lang) == 0);

		if(index == -1) index = 0;

		this.View.LanguageSelection.value = index;
	}

	protected virtual void OnLangChanged(string value) {
		if(this.View == null) return;

		this.View.Lang = value;
	}

	protected virtual void OnLanguageSelectionChanged(int index) {
		var lang = View.LanguageSelection.options[index].text;

		DataManager.Instance.AppData.Lang = lang;
	}
}