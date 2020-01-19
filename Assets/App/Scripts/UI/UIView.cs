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

	protected virtual void UpdateLocalization() { }
}