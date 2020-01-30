using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : UIController<GameOverView> {
	protected override void Attach() {
		this.View.Retry.onClick.AddListener(this.OnRetryClicked);
		this.View.Quit.onClick.AddListener(this.OnQuitClicked);

		this.View.Score.text = GameManager.Score.ToString();

		var span = System.TimeSpan.FromSeconds(GameManager.TotalTime);

		if(span.Hours > 0) {
			this.View.Time.text = string.Format("{0:00}:{1:00}:{2:00}", span.Hours, span.Minutes, span.Seconds);
		} else {
			this.View.Time.text = string.Format("{0:00}:{1:00}", span.Minutes, span.Seconds);
		}
	}

	protected override void Detach() {
		this.View.Retry.onClick.RemoveListener(this.OnRetryClicked);
		this.View.Quit.onClick.RemoveListener(this.OnQuitClicked);
	}

	protected virtual void OnRetryClicked() {
		// Lazy way...
		SceneManager.LoadScene("Game");
	}

	protected virtual void OnQuitClicked() {
		SceneManager.LoadScene("MainMenu");
	}
}