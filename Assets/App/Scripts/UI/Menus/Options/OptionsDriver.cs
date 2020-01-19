using UnityEngine;

public class OptionsDriver : MonoBehaviour {
	[SerializeField]
	private OptionsController controller;
	[SerializeField]
	private OptionsView view;

	private void Start() {
		this.controller.View = this.view;
	}
}