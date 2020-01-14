using UnityEngine;

public class GameManager : Singleton<GameManager> {
	public static event System.Action<int> onScoreChanged = null;
	private int score = 0;

	public static int Score {
		get {
			return Instance.score;
		}
	}

	public static void OnPickedUp(Pickup pickup) {
		if(pickup is Pellet) {
			Instance.score += 10;

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
		this.maker = Object.FindObjectOfType < MazeMaker>();
	}
}