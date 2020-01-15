using UnityEngine;

public static class Ini {
	[RuntimeInitializeOnLoadMethod]
	static void Run() {
		Application.targetFrameRate = 60;
	}
}
