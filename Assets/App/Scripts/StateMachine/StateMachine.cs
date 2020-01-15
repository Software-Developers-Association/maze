using System.Collections.Generic;

public delegate bool State(Process process);

[System.Serializable]
public class StateMachine : System.Object {
	private Dictionary<string, State> states = new Dictionary<string, State>();
	private string stateId;
	private State state;

	public string State {
		get {
			return this.stateId;
		} set {
			this.Transition(value);
		}
	}

	public void Add(string stateId, State state) {
		if(this.states.ContainsKey(stateId) == false) {
			this.states.Add(stateId, null);
		}

		this.states[stateId] = state;
	}

	public bool Remove(string stateId) {
		return this.states.Remove(stateId);
	}

	protected virtual bool Transition(string stateId) {
		if(string.IsNullOrEmpty(stateId)) return false;

		if(string.Compare(this.State, stateId) == 0) return false;

		if(this.states.ContainsKey(stateId) == false) return false;

		if(this.state != null) {
			if(this.state(Process.Exit) == false) {
				return false;
			}
		}

		this.stateId = stateId;

		this.state = this.states[this.stateId];

		return this.state(Process.Enter);
	}

	public virtual void Update() {
		if(this.state == null) return;

		this.state(Process.Update);
	}
}