using UnityEngine;

public class GameManager : Singleton<GameManager> {
	public static event System.Action<int> onScoreChanged = null;
	private int score = 0;
	private float seconds;

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

			if(Instance.score % 100 == 0) {
				Instance.maker.Generate();
			}
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