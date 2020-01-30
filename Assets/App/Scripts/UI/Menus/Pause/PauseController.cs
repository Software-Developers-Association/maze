using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : UIController<PauseView> {
	protected override void Attach() {
		this.View.Options.onClick.AddListener(this.OnOptionsClicked);
		this.View.Quit.onClick.AddListener(this.OnQuitClicked);
	}

	protected override void Detach() {
		this.View.Options.onClick.RemoveListener(this.OnOptionsClicked);
		this.View.Quit.onClick.RemoveListener(this.OnQuitClicked);
	}

	protected virtual void OnOptionsClicked() {
		UIManager.Open<OptionsController>("Options");
	}

	protected virtual void OnQuitClicked() {
		SceneManager.LoadScene("MainMenu");
	}

	public override void Show(bool show) {
		this.View.gameObject.SetActive(show);
	}

	protected virtual void OnEnable() {
		GameManager.Pause(true);
	}

	protected virtual void OnDisable() {
		GameManager.Pause(false);
	}
}