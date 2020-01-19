using UnityEngine;

public abstract class Actor : MonoBehaviour {
	private StateMachine stateMachine = new StateMachine();

	public StateMachine StateMachine {
		get {
			return this.stateMachine;
		}
	}

	protected virtual void Update() { this.UpdateStateMachine(); }

	protected virtual void UpdateStateMachine() {
		this.stateMachine.Update();
	}
}