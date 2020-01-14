using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Pickup : MonoBehaviour {
	protected virtual void OnTriggerEnter(Collider collider) {
		this.OnSelected(collider);
	}

	protected abstract void OnSelected(Collider collider);
}