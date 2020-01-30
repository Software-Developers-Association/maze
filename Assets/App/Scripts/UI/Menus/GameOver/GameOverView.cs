using UnityEngine;
using UnityEngine.UI;

public class GameOverView : UIView {
	[SerializeField]
	private Text score;
	[SerializeField]
	private Text time;
	[SerializeField]
	private Button retry;
	[SerializeField]
	private Button quit;

	public Text Score {
		get {
			return this.score;
		}
	}

	public Text Time {
		get {
			return this.time;
		}
	}

	public Button Retry {
		get {
			return this.retry;
		}
	}

	public Button Quit {
		get {
			return this.quit;
		}
	}
}