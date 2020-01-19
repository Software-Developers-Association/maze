using UnityEngine;

public class CamFollow : MonoBehaviour {
	Transform player;

	private void Start() {
		var go = GameObject.FindWithTag("Player");

		if(go) {
			this.player = go.transform;
		} else {
			this.enabled = false;
		}
	}

	private void Update() {
		var position = this.player.position;

		position.x /= 25.0F;
		position.y = this.transform.position.y;
		position.z /= 25.0F;

		this.transform.position = Vector3.Lerp(this.transform.position, position, 10.0F * Time.deltaTime);
	}
}
