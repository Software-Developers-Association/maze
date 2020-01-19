using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Virus : Actor {
	public Patrol patrol;

	protected virtual void Start() {
		this.patrol = new Patrol(1 << LayerMask.NameToLayer("Wall"), this.gameObject);

		this.StateMachine.Add("Patrol", this.patrol.State);

		this.StartCoroutine(this._Wait());
	}

	IEnumerator _Wait() {
		yield return new WaitForSeconds(0.25F);

		this.StateMachine.State = "Patrol";
	}

	protected virtual void OnTriggerEnter(Collider collider) {
		GameManager.OnContacted(this);
	}
}
