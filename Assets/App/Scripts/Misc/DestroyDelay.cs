using UnityEngine;
using System.Collections;

public class DestroyDelay : MonoBehaviour {
	[SerializeField]
	private float delay = 1.0F;

	private void Start() {
		this.StartCoroutine(this._Delay());
	}

	IEnumerator _Delay() {
		yield return new WaitForSeconds(this.delay);

		Destroy(this.gameObject);
	}
}