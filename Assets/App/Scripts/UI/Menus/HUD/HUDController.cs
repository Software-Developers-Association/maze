using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : UIController<HUDView> {
	static readonly string TimeDisplay = "{0:00}:{1:00}";

	protected override void Attach() {
		this.View.Pause.onClick.AddListener(this.OnPauseEvent);

		this.StartCoroutine("Clock");

		GameManager.onScoreChanged += this.OnScoreChanged;
		GameManager.onPowerupChanged += this.OnPowerupChanged;
	}

	protected override void Detach() {
		this.StopCoroutine("Clock");

		this.View.Pause.onClick.RemoveListener(this.OnPauseEvent);

		GameManager.onScoreChanged -= this.OnScoreChanged;
		GameManager.onPowerupChanged -= this.OnPowerupChanged;
	}

	protected virtual void OnPauseEvent() {
		// TODO: Show PauseMenu
		UIManager.Open<PauseController>("Pause");
	}

	protected virtual void OnScoreChanged(int value) {
		this.View.Score.text = value.ToString();
	}

	protected virtual void OnPowerupChanged(float value) {
		this.View.Powerup.value = value;
	}

	private IEnumerator Clock() {
		float lastTime = GameManager.Seconds;

		var span = System.TimeSpan.FromSeconds(GameManager.Seconds);

		this.View.Time.text = string.Format(TimeDisplay, span.Minutes, span.Seconds);

		while(true) {
			var delta = Mathf.Abs(GameManager.Seconds - lastTime);

			// only update the UI every second (avoid excessive garbage collection from string allocations)
			if(delta >= 1.0F) {
				span = System.TimeSpan.FromSeconds(GameManager.Seconds);

				this.View.Time.text = string.Format(TimeDisplay, span.Minutes, span.Seconds);

				if(GameManager.Seconds >= 10.0F) {
					this.View.Time.color = Color.white;
				} else if(GameManager.Seconds >= 5.0F) {
					this.View.Time.color = Color.yellow;
				} else {
					this.View.Time.color = Color.red;
				}

				lastTime = GameManager.Seconds;
			}

			yield return null;
		}
	}

	public override bool OnBack() {
		UIManager.Open("Pause");

		return false;
	}

	public override void Close(System.Action callback) {
		GameManager.onScoreChanged -= this.OnScoreChanged;
		GameManager.onPowerupChanged -= this.OnPowerupChanged;

		base.Close(callback);
	}

	private void OnDestroy() {
		GameManager.onScoreChanged -= this.OnScoreChanged;
		GameManager.onPowerupChanged -= this.OnPowerupChanged;
	}
}
