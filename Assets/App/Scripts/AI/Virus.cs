using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Virus : Actor {
	public Patrol patrol;

	protected virtual void Start() {
		this.patrol = new Patrol(1 << LayerMask.NameToLayer("Wall"), this.gameObject);

		this.stateMachine.Add("Patrol", this.patrol.State);

		this.stateMachine.State = "Patrol";
	}

	protected virtual void OnTriggerEnter(Collider collider) {
		GameManager.OnHitVirus();
	}
}
