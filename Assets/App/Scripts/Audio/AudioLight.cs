using UnityEngine;

[RequireComponent(typeof(Light))]
public class AudioLight : ReactiveAudio {
	[SerializeField]
	private Vector2 intensity;
	[SerializeField]
	private Vector2 range;

	private new Light light;

	private void Awake() {
		this.light = this.GetComponent<Light>();
	}

	protected override void Update() {
		base.Update();

		this.light.intensity = this.intensity.x + this.intensity.y * this.Value;
		this.light.range = this.range.x + this.range.y * this.Value;
	}
}
