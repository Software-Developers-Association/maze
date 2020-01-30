using UnityEngine;

[System.Serializable]
public class AppData : Observable {
	public AppData() {
		this.Lang = "en";
	}

	public string Lang {
		get {
			string lang = string.Empty;

			if(this.GetValue("Lang", out lang)) {
				return lang;
			}

			return lang;
		} set {
			if(this.OnUpdate("Lang", value)) {
				Debug.Log("Lang Changed to: " + value);
			}
		}
	}
}