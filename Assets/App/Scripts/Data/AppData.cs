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
		}
		set {
			this.OnUpdate("Lang", value);
		}
	}
}