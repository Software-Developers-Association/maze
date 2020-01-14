public class MazeGenerator : System.Object {
	private int width, height;
	private IMaze algorithm;

	public MazeGenerator() {
		this.width = this.height = 0;
	}

	public MazeGenerator(int width, int height) {
		this.width = width;
		this.height = height;
	}

	public int Width {
		get {
			return this.width;
		} set {
			this.width = value < 0 ? 0 : value;
		}
	}

	public int Height {
		get {
			return this.height;
		} set {
			this.height = value < 0 ? 0 : value;
		}
	}

	public IMaze Algorithm {
		get {
			return this.algorithm;
		} set {
			this.algorithm = value;
		}
	}

	public Cell[,] Generate(IMaze iMaze, int x, int y) {
		return iMaze.Create(this.Width, this.Height, x, y);
	}

	public Cell[,] Generate(int x, int y) {
		return this.algorithm.Create(this.Width, this.Height, x, y);
	}
}