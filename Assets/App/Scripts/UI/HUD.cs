using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	[SerializeField]
	private Text score;

	private void Start() {
		GameManager.onScoreChanged += GameManager_onScoreChanged;
	}

	private void GameManager_onScoreChanged(int score) {
		this.score.text = score.ToString();
	}
}
