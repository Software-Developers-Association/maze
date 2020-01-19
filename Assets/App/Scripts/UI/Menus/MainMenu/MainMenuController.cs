using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : UIController<MainMenuView> {
	protected override void Detach(MainMenuView view) {
		if(view == null) return;

		view.Play.onClick.RemoveListener(OnPlay);
		view.Score.onClick.RemoveListener(OnScore);
		view.Options.onClick.RemoveListener(OnOptions);
		view.Credits.onClick.RemoveListener(OnCredits);
		view.Menu.onClick.RemoveListener(OnMenu);
	}

	protected override void Attach(MainMenuView view) {
		if(view == null) return;

		view.Play.onClick.AddListener(OnPlay);
		view.Score.onClick.AddListener(OnScore);
		view.Options.onClick.AddListener(OnOptions);
		view.Credits.onClick.AddListener(OnCredits);
		view.Menu.onClick.AddListener(OnMenu);

		view.Score.interactable = false;
		//view.Options.interactable = false;
		view.Credits.interactable = false;
		view.Menu.interactable = false;
	}

	protected virtual void OnPlay() {
		SceneManager.LoadScene("Game");
	}

	protected virtual void OnScore() {

	}

	protected virtual void OnOptions() {
		UIManager.Open<OptionsController>("Options");
	}

	protected virtual void OnCredits() {

	}

	protected virtual void OnMenu() {

	}
}