using UnityEngine;

public class SpectrumListener : MonoBehaviour {
	private static SpectrumListener instance = null;

	private float[] samples = new float[512];
	private float[] bands = new float[8];

	private void Awake() {
		instance = this;

#if UNITY_WEBGL && !UNITY_EDITOR
		SSWebInteract.SetFFTSize(this.samples.Length);
#endif
	}

	private void Update() {
		if(MusicPlayer.Current == null) return;

#if UNITY_WEBGL && !UNITY_EDITOR
		SSWebInteract.GetSpectrumData(this.samples);

		// attempt Triangular FFT
		for(int i = 0; i < this.samples.Length; ++i) {
			this.samples[i] *= (1.0F - Mathf.Abs((i - this.samples.Length / 2) / this.samples.Length / 2));
		}
#else
		MusicPlayer.Current.GetSpectrumData(this.samples, 0, FFTWindow.Triangle);
#endif

		int sampleIndex = 0;

		for(int i = 0; i < this.bands.Length; ++i) {
			float average = 0.0F;

			int sampleCount = (int)Mathf.Pow(2, i) * 2;

			if(i == 7) {
				sampleCount += 2;
			}

			for(int j = 0; j < sampleCount; ++j) {
				average += samples[sampleIndex++] * (sampleIndex);

				average /= sampleIndex;
			}

			bands[i] = average * 10.0F;
		}
	}

	public static float GetBand(Band band) {
		if(instance == null) return 0.0F;

		return instance.bands[(int)band];
	}

	public enum Band {
		SubBass = 0,
		Bass,
		LowMidRange,
		MidRange,
		UpperMidRange,
		Presence,
		Brilliance
	}
}
