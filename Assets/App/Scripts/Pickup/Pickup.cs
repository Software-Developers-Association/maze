using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public abstract class Pickup : MonoBehaviour {
	protected virtual void OnTriggerEnter(Collider collider) {
		this.StartCoroutine(this._Notify(collider));
	}

	private IEnumerator _Notify(Collider collider) {
		yield return new WaitForEndOfFrame();

		this.OnSelected(collider);
	}

	protected abstract void OnSelected(Collider collider);
}