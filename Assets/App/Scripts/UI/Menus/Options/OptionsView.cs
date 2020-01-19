using UnityEngine;
using UnityEngine.UI;

public class OptionsView : UIView {
	[SerializeField, Header("Labels")]
	private Text header;
	[SerializeField]
	private Text language;
	[SerializeField]
	private Dropdown languageSelection;

	public Text Header {
		get {
			return this.header;
		}
	}

	public Text Language {
		get {
			return this.language;
		}
	}

	public Dropdown LanguageSelection {
		get {
			return this.languageSelection;
		}
	}

	protected override void UpdateLocalization() {
		if(this.Localization == null) return;

		Localization.Phrase phrase;
		Localization.Phrase.Local local;

		if(this.header != null) {
			if(this.Localization.GetPhrase("Header", out phrase)) {
				if(phrase.GetLocal(this.Lang, out local)) {
					this.header.text = local.Text;
				}
			}
		}

		if(this.language != null) {
			if(this.Localization.GetPhrase("Language", out phrase)) {
				if(phrase.GetLocal(this.Lang, out local)) {
					this.language.text = local.Text;
				}
			}
		}
	}

	protected virtual void OnValidate() {
		this.UpdateLocalization();
	}
}