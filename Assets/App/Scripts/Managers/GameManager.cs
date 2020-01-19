﻿using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
	public static event System.Action<int> onScoreChanged = null;
	public static event System.Action<float> onPowerupChanged = null;

	private int score = 0;
	private float seconds;
	private int pickups = 0;
	private float powerup;
	private bool isInvinsible = false;

	public static int Score {
		get {
			return Instance.score;
		} private set {
			Instance.score = value;

			Mathf.Clamp(Instance.score, 0, int.MaxValue);

			if(GameManager.onScoreChanged != null) {
				GameManager.onScoreChanged(Instance.score);
			}
		}
	}

	public static float Seconds {
		get {
			return Instance.seconds;
		}
	}

	public static float Powerup {
		get {
			return Instance.powerup;
		} private set {
			if(Instance.powerup != value) {
				Instance.powerup = value;

				if(GameManager.onPowerupChanged != null) {
					GameManager.onPowerupChanged(Instance.powerup);
				}
			}
		}
	}

	public static void OnPickedUp(Pickup pickup) {
		if(pickup is Pellet) {
			GameManager.Score += 10;
			Instance.seconds += 5.0F;

			Instance.pickups++;

			if(Instance.pickups == 10) {
				Instance.pickups = 0;
				Instance.maker.Generate();
			}
		}
	}

	public static bool OnPowerUp() {
		if(GameManager.Powerup < 1.0F) return false;

		GameManager.Powerup = 0.0F;
		Instance.isInvinsible = true;

		Instance.Invoke("ResetPowerup", 1.0F);

		return true;
	}

	public static void OnHitVirus() {
		if(Instance.isInvinsible == true) {
			GameManager.Score += 5;

			return;
		}

		GameManager.Score -= 5;
	}

	public static void OnContacted(Actor actor) {
		if(actor is Virus) {
			GameManager.OnHitVirus();
			Destroy(actor.gameObject);
		}
	}

	private MazeMaker maker = null;

	private void Start() {
		this.maker = Object.FindObjectOfType <MazeMaker>();

		this.seconds = 30.0F;

		this.StartCoroutine(this._ChargePowerup());
	}

	private IEnumerator _ChargePowerup() {
		var eTime = 0.0F;

		while(eTime < 3.0F) {
			eTime += Time.deltaTime;

			GameManager.Powerup = eTime / 3.0F;

			yield return null;
		}

		GameManager.Powerup = 1.0F;
	}

	private void ResetPowerup() {
		this.isInvinsible = false;

		this.StartCoroutine(this._ChargePowerup());
	}

	private void Update() {
		this.seconds -= Time.deltaTime;

		if(this.seconds <= 0.0F) this.seconds = 0.0F;
	}
}