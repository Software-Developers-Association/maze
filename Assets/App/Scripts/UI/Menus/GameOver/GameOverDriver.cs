using UnityEngine;

[RequireComponent(typeof(GameOverController))]
public class GameOverDriver : MonoBehaviour {
	[SerializeField]
	private GameOverView view;

	private void Start() {
		var controller = this.GetComponent<GameOverController>();

		controller.View = this.view;
	}
}