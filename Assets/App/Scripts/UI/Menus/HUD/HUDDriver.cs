using UnityEngine;

[RequireComponent(typeof(HUDController))]
public class HUDDriver : MonoBehaviour {
	[SerializeField]
	private HUDView view;

	private void Start() {
		var controller = this.GetComponent<HUDController>();

		controller.View = this.view;
	}
}
