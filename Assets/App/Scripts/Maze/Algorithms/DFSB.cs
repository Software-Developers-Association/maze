/// <summary>
/// Depth First Search Backtracking
/// </summary>
[System.Serializable]
public class DFSB : IMaze {
	private System.Random random; // A Random class to generate random numbers.
	private Cell[,] cells; // cache the grid for recursive call.

	/// <summary>
	/// DFSB requires a seed for the Random class object.
	/// </summary>
	/// <param name="seed">The seed for the Random class object.</param>
	public DFSB(int seed) {
		this.random = new System.Random(seed);
	}

	public DFSB(System.Random random) {
		this.random = random;
	}

	public System.Random Random {
		get {
			return this.random;
		} set {
			this.random = value;
		}
	}

	/// <summary>
	/// Creates and returns a 2D array of type <seealso cref="Cell"/>.
	/// </summary>
	/// <param name="width">The width of the grid</param>
	/// <param name="height">The height of the grid</param>
	/// <param name="x">The starting cell x index</param>
	/// <param name="y">The starting cell y index</param>
	/// <returns></returns>
	public Cell[,] Create(int width, int height, int x, int y) {
		this.cells = new Cell[width, height];

		// rows
		for(int j = 0; j < this.cells.GetLength(1); ++j) {
			// columns.
			for(int i = 0; i < this.cells.GetLength(0); ++i) {
				this.cells[i, j] = new Cell {
					visited = false,
					walls = Cell.Wall.North | Cell.Wall.South | Cell.Wall.East | Cell.Wall.West,
					x = i,
					y = j
				};
			}
		}

		this.Step(this.cells[x, y]);

		return this.cells;
	}

	private void Step(Cell cell) {
		cell.visited = true;

		Cell.Wall neighbors = this.GetValidNeighbors(cell);

		// while there are neighbors to check...
		while(neighbors != Cell.Wall.None) {
			// choose a neighbor at random.
			var neighbor = (Cell.Wall)(1 << this.random.Next(4));

			// if the neighbor is not a neighbor we can visit
			// skip this iteration...
			if((neighbors & neighbor) != neighbor) continue;

			// which neighbor are we checking?
			switch(neighbor) {
				case Cell.Wall.North:
					// we must remove the walls between the current cell
					// and the cell we are going to visit...
					cell.walls &= ~Cell.Wall.North;
					this.cells[cell.x, cell.y - 1].walls &= ~Cell.Wall.South;

					this.Step(this.cells[cell.x, cell.y - 1]);
					break;
				case Cell.Wall.South:
					// we must remove the walls between the current cell
					// and the cell we are going to visit...
					cell.walls &= ~Cell.Wall.South;
					this.cells[cell.x, cell.y + 1].walls &= ~Cell.Wall.North;

					this.Step(this.cells[cell.x, cell.y + 1]);
					break;
				case Cell.Wall.East:
					// we must remove the walls between the current cell
					// and the cell we are going to visit...
					cell.walls &= ~Cell.Wall.East;
					this.cells[cell.x + 1, cell.y].walls &= ~Cell.Wall.West;

					this.Step(this.cells[cell.x + 1, cell.y]);
					break;
				case Cell.Wall.West:
					// we must remove the walls between the current cell
					// and the cell we are going to visit...
					cell.walls &= ~Cell.Wall.West;
					this.cells[cell.x - 1, cell.y].walls &= ~Cell.Wall.East;

					this.Step(this.cells[cell.x - 1, cell.y]);
					break;
			}

			// Update the neighbors we can visit...
			neighbors = this.GetValidNeighbors(cell);
		}
	}

	/// <summary>
	/// Gets a flag of all valid neighboring cells.
	/// </summary>
	/// <param name="cell">The cell to check.</param>
	/// <returns>A BitFlag with all the valid neighbors.</returns>
	private Cell.Wall GetValidNeighbors(Cell cell) {
		var neighbors = Cell.Wall.All;

		// check the north..
		if(cell.y - 1 < 0 || this.cells[cell.x, cell.y - 1].visited) {
			neighbors &= ~Cell.Wall.North;
		}

		// check the south
		if(cell.y + 1 >= this.cells.GetLength(1) || this.cells[cell.x, cell.y + 1].visited) {
			neighbors &= ~Cell.Wall.South;
		}

		// check the east
		if(cell.x + 1 >= this.cells.GetLength(0) || this.cells[cell.x + 1, cell.y].visited) {
			neighbors &= ~Cell.Wall.East;
		}

		// check the west
		if(cell.x - 1 < 0 || this.cells[cell.x - 1, cell.y].visited) {
			neighbors &= ~Cell.Wall.West;
		}

		return neighbors;
	}
}