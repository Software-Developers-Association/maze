using UnityEngine;

public class Pellet : Pickup {
	protected override void OnSelected(Collider collider) {
		this.gameObject.SetActive(false);
	}
}