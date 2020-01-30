using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : UIController<MainMenuView> {
	public override bool OnBack() {
		// ensure the MainMenu is always the root.
		return false;
	}

	protected override void Detach() {
		this.View.Play.onClick.RemoveListener(OnPlayEvent);
		this.View.Score.onClick.RemoveListener(OnScoreEvent);
		this.View.Options.onClick.RemoveListener(OnOptionsEvent);
		this.View.Credits.onClick.RemoveListener(OnCreditsEvent);
		this.View.Menu.onClick.RemoveListener(OnMenuEvent);
	}

	protected override void Attach() {
		this.View.Play.onClick.AddListener(OnPlayEvent);
		this.View.Score.onClick.AddListener(OnScoreEvent);
		this.View.Options.onClick.AddListener(OnOptionsEvent);
		this.View.Credits.onClick.AddListener(OnCreditsEvent);
		this.View.Menu.onClick.AddListener(OnMenuEvent);

		this.View.Score.interactable = false;
		//view.Options.interactable = false;
		//this.View.Credits.interactable = false;
		this.View.Menu.interactable = false;
	}

	protected virtual void OnPlayEvent() {
		SceneManager.LoadScene("Game");
	}

	protected virtual void OnScoreEvent() {

	}

	protected virtual void OnOptionsEvent() {
		UIManager.Open<OptionsController>("Options");
	}

	protected virtual void OnCreditsEvent() {
		UIManager.Open("Credits");
	}

	protected virtual void OnMenuEvent() {

	}
}