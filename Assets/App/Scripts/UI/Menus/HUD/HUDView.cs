using UnityEngine;
using UnityEngine.UI;

public class HUDView : UIView {
	[SerializeField, Header("HUDView")]
	private Text time;
	[SerializeField]
	private Text score;
	[SerializeField]
	private Slider powerup;
	[SerializeField]
	private Button pause;

	public Text Time {
		get {
			return this.time;
		}
	}

	public Text Score {
		get {
			return this.score;
		}
	}

	public Slider Powerup {
		get {
			return this.powerup;
		}
	}

	public Button Pause {
		get {
			return this.pause;
		}
	}
}