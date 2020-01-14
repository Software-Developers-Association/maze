using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	static readonly string MinSec = "{0:00}:{1:00}";
	static readonly string SecMil = "{0:00}:{1:000}";

	[SerializeField]
	private Text score;
	[SerializeField]
	private Text time;

	private void Start() {
		GameManager.onScoreChanged += GameManager_onScoreChanged;
	}

	private void GameManager_onScoreChanged(int score) {
		this.score.text = score.ToString();
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
