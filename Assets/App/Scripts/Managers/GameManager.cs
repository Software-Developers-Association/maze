using UnityEngine;

public class GameManager : Singleton<GameManager> {
	public static event System.Action<int> onScoreChanged = null;
	private int score = 0;
	private float seconds;
	private int pickups = 0;

	public static int Score {
		get {
			return Instance.score;
		}
	}

	public static float Seconds {
		get {
			return Instance.seconds;
		}
	}

	public static void OnPickedUp(Pickup pickup) {
		if(pickup is Pellet) {
			Instance.score += 10;
			Instance.seconds += 5.0F;

			if(GameManager.onScoreChanged != null) {
				GameManager.onScoreChanged(GameManager.Score);
			}

			Instance.pickups++;

			if(Instance.pickups == 10) {
				Instance.maker.Generate();
			}
		}
	}

	public static void OnHitVirus() {
		Instance.score -= 5;

		Instance.score = Mathf.Clamp(Instance.score, 0, int.MaxValue);

		if(GameManager.onScoreChanged != null) {
			GameManager.onScoreChanged(GameManager.Score);
		}
	}

	private MazeMaker maker = null;

	private void Start() {
		this.maker = Object.FindObjectOfType <MazeMaker>();

		this.seconds = 30.0F;
	}

	private void Update() {
		this.seconds -= Time.deltaTime;

		if(this.seconds <= 0.0F) this.seconds = 0.0F;
	}
}