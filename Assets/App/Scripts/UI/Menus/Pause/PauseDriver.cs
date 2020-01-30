using UnityEngine;

[RequireComponent(typeof(PauseController))]
public class PauseDriver : MonoBehaviour {
	[SerializeField]
	private PauseView view;

	private void Start() {
		var controller = this.GetComponent<PauseController>();

		controller.View = this.view;
	}
}