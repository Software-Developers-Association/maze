using UnityEngine;

public class Player : Actor {
	[SerializeField]
	private MeshRenderer meshRenderer;
	[SerializeField]
	private TrailRenderer trailRenderer;
	[SerializeField]
	private Color normal = Color.cyan, empowered = Color.magenta;

	protected virtual void Start() {
		this.StateMachine.Add("Normal", this.State_Normal);
		this.StateMachine.Add("Empowered", this.State_Empowered);
	}

	protected virtual bool State_Normal(Process process) {
		if(process == Process.Enter) {
			this.meshRenderer.material.color = this.normal;
			this.trailRenderer.startColor = this.trailRenderer.endColor = this.normal;
		}

		return true;
	}

	protected virtual bool State_Empowered(Process process) {
		if(process == Process.Enter) {
			this.meshRenderer.material.color = this.empowered;
			this.trailRenderer.startColor = this.trailRenderer.endColor = this.empowered;
		}

		return true;
	}
}