using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static GameManager instance = null;

	public static event System.Action<int> onScoreChanged = null;
	public static event System.Action<float> onPowerupChanged = null;

	[SerializeField]
	private GameObject uiScore;
	[SerializeField]
	private Player player;

	private int score = 0;
	private float seconds = 0.0F;
	private float totalTime = 0.0F;
	private int pickups = 0;
	private float powerup = 0.0F;
	private bool isInvinsible = false;

	public static int Score {
		get {
			return instance.score;
		} private set {
			instance.score = value;

			Mathf.Clamp(instance.score, 0, int.MaxValue);

			if(GameManager.onScoreChanged != null) {
				GameManager.onScoreChanged(instance.score);
			}
		}
	}

	public static float Seconds {
		get {
			return instance.seconds;
		}
	}

	public static float TotalTime {
		get {
			return instance.totalTime;
		}
	}

	public static float Powerup {
		get {
			return instance.powerup;
		} private set {
			if(instance.powerup != value) {
				instance.powerup = value;

				if(GameManager.onPowerupChanged != null) {
					GameManager.onPowerupChanged(instance.powerup);
				}
			}
		}
	}

	public static void OnPickedUp(Pickup pickup) {
		if(pickup is Pellet) {
			GameManager.Score += 10;

			var clone = GameObject.Instantiate(instance.uiScore);

			clone.transform.position = pickup.transform.position;

			clone.GetComponentInChildren<UnityEngine.UI.Text>().text = "+10";

			instance.pickups++;

			if(instance.pickups == 10) {
				instance.pickups = 0;
				instance.maker.Generate();
				instance.seconds += 15.0F;
			} else {
				instance.seconds += 5.0F;
			}
		}
	}

	public static bool OnPowerUp() {
		if(GameManager.Powerup < 1.0F) return false;

		GameManager.Powerup = 0.0F;
		instance.isInvinsible = true;

		instance.player.StateMachine.State = "Empowered";

		instance.Invoke("ResetPowerup", 2.5F);

		return true;
	}

	public static void OnHitVirus() {
		if(instance.isInvinsible == true) {
			GameManager.Score += 5;

			var clone = GameObject.Instantiate(instance.uiScore);

			clone.transform.position = instance.player.transform.position;

			clone.GetComponentInChildren<UnityEngine.UI.Text>().text = "+5";

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

	public static void Pause(bool pause) {
		if(pause) {
			Time.timeScale = 0.0F;
		} else {
			Time.timeScale = 1.0F;
		}
	}

	private MazeMaker maker = null;

	private void Awake() {
		GameManager.instance = this;
	}

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

		instance.player.StateMachine.State = "Normal";
	}

	private void OnDestroy() {
		GameManager.Pause(false);
	}

	private void Update() {
		this.seconds -= Time.deltaTime;
		this.totalTime += Time.deltaTime;

		if(this.seconds <= 0.0F) {
			this.seconds = 0.0F;

			Debug.LogWarning("Game Over");

			GameManager.Pause(true);

			UIManager.Open("GameOver");

			this.gameObject.SetActive(false);
		}
	}
}