using UnityEngine;

public abstract class Actor : MonoBehaviour {
	protected StateMachine stateMachine = new StateMachine();

	protected virtual void Update() {
		this.stateMachine.Update();
	}
}