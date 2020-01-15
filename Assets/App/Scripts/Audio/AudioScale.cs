using UnityEngine;

public class AudioScale : ReactiveAudio {
	[SerializeField]
	private Axis axis = Axis.XAxis;
	[SerializeField]
	private Vector3 minScale = Vector3.one;
	[SerializeField]
	private Vector3 maxScale = Vector3.one;

	protected override void Update() {
		base.Update();

		var scale = Vector3.one;

		if((this.axis & Axis.XAxis) != Axis.None) {
			//scale.x = this.minScale.x + this.Value;
			scale.x = Mathf.Lerp(this.minScale.x, this.maxScale.x, this.Value);
		}

		if((this.axis & Axis.YAxis) != Axis.None) {
			//scale.y = this.minScale.y + this.Value;
			scale.y = Mathf.Lerp(this.minScale.y, this.maxScale.y, this.Value);
		}

		if((this.axis & Axis.ZAxis) != Axis.None) {
			//scale.z = this.minScale.z + this.Value;
			scale.z = Mathf.Lerp(this.minScale.z, this.maxScale.z, this.Value);
		}

		//var current = this.transform.localScale;

		//this.transform.localScale = Vector3.Lerp(current, scale, Time.unscaledDeltaTime * this.Lerp);
		this.transform.localScale = scale;
	}

	[System.Flags]
	public enum Axis {
		None = 0,
		XAxis = 1 << 0,
		YAxis = 1 << 1,
		ZAxis = 1 << 3,
		All = Axis.XAxis | Axis.YAxis | Axis.ZAxis
	}
}
