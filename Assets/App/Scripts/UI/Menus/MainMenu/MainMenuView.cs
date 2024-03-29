﻿using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : UIView {
	[Header("Labels")]
	[SerializeField]
	private Text header;
	[SerializeField, Header("Buttons")]
	private Button play;
	[SerializeField]
	private Button score;
	[SerializeField]
	private Button options;
	[SerializeField]
	private Button credits;
	[SerializeField]
	private Button menu;

	public Text Header {
		get {
			return this.header;
		}
	}

	public Button Play {
		get {
			return this.play;
		}
	}

	public Button Score {
		get {
			return this.score;
		}
	}

	public Button Options {
		get {
			return this.options;
		}
	}

	public Button Credits {
		get {
			return this.credits;
		}
	}

	public Button Menu {
		get {
			return this.menu;
		}
	}

	protected override void UpdateLocalization() {
		if(this.Localization == null) return;

		Localization.Phrase phrase;
		Localization.Phrase.Local local;

		if(this.Localization.GetPhrase("Header", out phrase)) {
			if(phrase.GetLocal(this.Lang, out local)) {
				this.Header.text = local.Text;
			}
		}

		if(this.Localization.GetPhrase("Play", out phrase)) {
			if(phrase.GetLocal(this.Lang, out local)) {
				this.Play.GetComponentInChildren<Text>().text = local.Text;
			}
		}

		if(this.Localization.GetPhrase("Score", out phrase)) {
			if(phrase.GetLocal(this.Lang, out local)) {
				this.Score.GetComponentInChildren<Text>().text = local.Text;
			}
		}

		if(this.Localization.GetPhrase("Options", out phrase)) {
			if(phrase.GetLocal(this.Lang, out local)) {
				this.Options.GetComponentInChildren<Text>().text = local.Text;
			}
		}

		if(this.Localization.GetPhrase("Credits", out phrase)) {
			if(phrase.GetLocal(this.Lang, out local)) {
				this.Credits.GetComponentInChildren<Text>().text = local.Text;
			}
		}
	}

	protected virtual void OnValidate() {
		this.UpdateLocalization();
	}
}