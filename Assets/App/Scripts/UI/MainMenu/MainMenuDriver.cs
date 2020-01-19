using UnityEngine;

public class MainMenuDriver : MonoBehaviour {
	[SerializeField]
	private MainMenuController controller;
	[SerializeField]
	private MainMenuView view;

	private void Start() {
		this.controller.View = this.view;
	}
}