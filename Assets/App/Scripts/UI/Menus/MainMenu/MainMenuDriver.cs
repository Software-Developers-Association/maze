using UnityEngine;

[RequireComponent(typeof(MainMenuController))]
public class MainMenuDriver : MonoBehaviour {
	[SerializeField]
	private MainMenuView view;

	private void Start() {
		var controller = this.GetComponent<MainMenuController>();

		controller.View = this.view;
	}
}