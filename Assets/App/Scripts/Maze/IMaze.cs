public interface IMaze {
	Cell[,] Create(int width, int height, int x, int y);
	System.Random Random { get; }
}