using UnityEngine;
using UnityEngine.UI;

public class PauseView : UIView {
	[SerializeField]
	private Text header;
	[SerializeField]
	private Button options;
	[SerializeField]
	private Button quit;

	public Text Header {
		get {
			return this.header;
		}
	}

	public Button Options {
		get {
			return this.options;
		}
	}

	public Button Quit {
		get {
			return this.quit;
		}
	}

	protected override void UpdateLocalization() {
		Localization.Phrase phrase;
		Localization.Phrase.Local local;

		if(this.Localization.GetPhrase("Header", out phrase)) {
			if(phrase.GetLocal(this.Lang, out local)) {
				this.header.text = local.Text;
			}
		}

		if(this.Localization.GetPhrase("Options", out phrase)) {
			if(phrase.GetLocal(this.Lang, out local)) {
				this.Options.GetComponentInChildren<Text>().text = local.Text;
			}
		}

		if(this.Localization.GetPhrase("Quit", out phrase)) {
			if(phrase.GetLocal(this.Lang, out local)) {
				this.Quit.GetComponentInChildren<Text>().text = local.Text;
			}
		}
	}

	protected virtual void OnValidate() {
		this.UpdateLocalization();
	}
}