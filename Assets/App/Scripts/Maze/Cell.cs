[System.Serializable]
public class Cell {
	public Wall walls;
	public int x, y;
	public bool visited;

	[System.Flags]
	public enum Wall {
		None	= 0x0000,
		North	= 1 << 0,
		South	= 1 << 1,
		East	= 1 << 2,
		West	= 1 << 3,
		All = Wall.North | Wall.South | Wall.East | Wall.West
	}
}