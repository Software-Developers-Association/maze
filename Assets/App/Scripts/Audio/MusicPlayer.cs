using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	private static MusicPlayer instance = null;

	[SerializeField]
	private AudioSource current, next;
	[SerializeField]
	private float crossfade = 15.0F;
	[SerializeField, Range(0.0F, 100.0F)]
	private float maxVolume = 80.0F;
	[SerializeField]
	private bool random = false;
	[SerializeField]
	private AudioClip[] clips = new AudioClip[0];

	private int index = 0;

	private void Awake() {
		MusicPlayer.instance = this;

		this.current.volume = this.next.volume = 0.0F;
	}

	private void Start() {
		this.StartCoroutine(this._PlayBackLoop());
	}

	IEnumerator _PlayBackLoop() {
		if(this.random) {
			this.current.clip = this.clips[Random.Range(0, this.clips.Length)];
		} else {
			this.current.clip = this.clips[this.index];
		}

		this.current.Play();

		this.StartCoroutine(this._FadeVolume(this.current, 0.0F, this.maxVolume / 100.0F, this.crossfade));

		while(true) {
			// we want to fade between songs at least 5 seconds..
			var waitTime = Mathf.Clamp(this.current.clip.length - this.crossfade, 0.0F, this.current.clip.length);

			yield return new WaitForSecondsRealtime(waitTime);

			if(this.random) {
				this.next.clip = this.clips[Random.Range(0, this.clips.Length)];
			} else {
				// we need to load up the next song, and fade out the current song over 5 seconds.
				this.index++;

				if(this.index >= this.clips.Length) this.index = 0;

				this.next.clip = this.clips[this.index];
			}

			this.next.Play();

			this.StartCoroutine(this._FadeVolume(this.next, 0.0F, this.maxVolume / 100.0F, this.crossfade));

			this.StartCoroutine(this._FadeVolume(this.current, this.maxVolume / 100.0F, 0.0F, this.crossfade));

			// swap sources.

			var temp = this.next;

			this.next = this.current;

			this.current = temp;
		}
	}

	IEnumerator _FadeVolume(AudioSource source, float from, float to, float time) {
		float eTime = 0.0F;

		while(Mathf.Approximately(source.volume, to) == false) {
			eTime += Time.unscaledDeltaTime;

			float lerp = eTime / time;

			lerp = Mathf.Clamp(lerp, 0.0F, 1.0F);

			source.volume = Mathf.Lerp(from, to, lerp);

			yield return null;
		}
	}

	public static AudioSource Current {
		get {
			if(MusicPlayer.instance == null) return null;

			return MusicPlayer.instance.current;
		}
	}
}