using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	static readonly string MinSec = "{0:00}:{1:00}";
	static readonly string SecMil = "{0:00}:{1:000}";

	[SerializeField]
	private Text score;
	[SerializeField]
	private Text time;
	[SerializeField]
	private Slider powerup;

	private void Start() {
		GameManager.onScoreChanged += GameManager_onScoreChanged;
		GameManager.onPowerupChanged += GameManager_onPowerupChanged;

		this.GameManager_onScoreChanged(GameManager.Score);
	}

	private void GameManager_onPowerupChanged(float value) {
		this.powerup.value = value;
	}

	private void GameManager_onScoreChanged(int value) {
		this.score.text = value.ToString();
	}

	private void Update() {
		var span = System.TimeSpan.FromSeconds(GameManager.Seconds);

		if(span.Minutes > 0) {
			this.time.text = string.Format(HUD.MinSec, span.Minutes, span.Seconds);
		} else {
			this.time.text = string.Format(HUD.MinSec, span.Minutes, span.Seconds);
		}
	}
}
