using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour {
	[SerializeField]
	private Button play, score, credits;

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

	public Button Credits {
		get {
			return this.credits;
		}
	}
}