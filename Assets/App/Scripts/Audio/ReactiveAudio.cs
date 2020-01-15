using UnityEngine;

public abstract class ReactiveAudio : MonoBehaviour {
	[SerializeField]
	private SpectrumListener.Band band = SpectrumListener.Band.SubBass;
	[SerializeField]
	private float multiplier = 1.0F;
	[SerializeField]
	private float threshhold = 0.25F;
	[SerializeField]
	private float lerp = 20.0F;

	private float lastBand = 0.0F;
	private float value = 0.0F;

	protected SpectrumListener.Band Band {
		get {
			return this.band;
		} set {
			this.band = value;
		}
	}

	protected float Multiplier {
		get {
			return this.multiplier;
		} set {
			this.multiplier = value;
		}
	}

	protected float Lerp {
		get {
			return this.lerp;
		} set {
			this.lerp = value;
		}
	}

	protected float Value {
		get {
			return Mathf.Clamp(this.value, 0.0F, 1.25F);
		}
	}

	protected virtual void Update() {
		var band = SpectrumListener.GetBand(this.Band);

		if(band <= this.threshhold) band = 0.0F;

		if(this.lastBand < band) {
			this.value = band;
		}

		this.value = Mathf.Lerp(this.value, 0.0F, this.Lerp * Time.deltaTime);

		this.lastBand = band;
	}
}