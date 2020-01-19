using UnityEngine;

public abstract class UIView : MonoBehaviour {
	[Header("Localization")]
	[SerializeField, Lang]
	private string lang = "en";
	[SerializeField]
	private Localization localization;

	public string Lang {
		get {
			return this.lang;
		} set {
			this.lang = value;

			this.UpdateLocalization();
		}
	}

	public Localization Localization {
		get {
			return this.localization;
		} set {
			this.localization = value;

			this.UpdateLocalization();
		}
	}

	protected virtual void OnEnable() {
		DataManager.GetAppData().Subscribe<string>("Lang", this.OnLangChanged);

		this.OnLangChanged(DataManager.GetAppData().Lang);
	}

	protected virtual void OnDisable() {
		DataManager.GetAppData().Unsubscribe<string>("Lang", this.OnLangChanged);
	}

	protected virtual void UpdateLocalization() { }

	protected virtual void OnLangChanged(string lang) {
		this.Lang = lang;
	}
}