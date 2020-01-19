using UnityEngine;

public class MazeMaker : MonoBehaviour {
	public GameObject wall;
	public GameObject pellet;
	public Virus virus;
	public MazeGenerator generator = new MazeGenerator();
	public int width, height;
	public Transform mazeParent;
	public float size = 10.0F;

	private IMaze iMaze = null;
	private Cell[,] maze = null;
	private GameObject[] pellets = null;

	private void Start() {
		this.Generate();
	}

	private GameObject CreateWall() {
		var clone = Instantiate(this.wall);

		clone.transform.localScale = new Vector3(this.size, this.wall.transform.localScale.y, this.wall.transform.localScale.z);

		return clone;
	}

	public void Regenerate() {
		int size = this.mazeParent.childCount;

		for(int i = 0; i < size; ++i) {
			DestroyImmediate(this.mazeParent.GetChild(0).gameObject);
		}

		this.mazeParent.localPosition = Vector3.zero;
		var xOffset = -this.generator.Width * this.size / 2.0F;
		var zOffset = this.generator.Height * this.size / 2.1F;

		if(this.maze == null) return;

		for(int j = 0; j < this.maze.GetLength(1); ++j) {
			for(int i = 0; i < this.maze.GetLength(0); ++i) {
				var cell = this.maze[i, j];
				var parent = new GameObject(string.Format("{0}, {1}", i, j)).transform;
				parent.SetParent(this.mazeParent);

				if((cell.walls & Cell.Wall.North) == Cell.Wall.North) {
					var wall = this.CreateWall();
					wall.name = "North";
					wall.SetActive(true);
					wall.transform.SetParent(parent);

					Vector3 position = Vector3.zero;

					position.x = i * this.size + xOffset;
					position.z = -j * this.size + this.size / 2.0F + zOffset;

					wall.transform.forward = Vector3.forward;
					wall.transform.position = position;
				}

				if((cell.walls & Cell.Wall.South) == Cell.Wall.South) {
					var wall = this.CreateWall();
					wall.name = "South";
					wall.SetActive(true);
					wall.transform.SetParent(parent);

					Vector3 position = Vector3.zero;

					position.x = i * this.size + xOffset;
					position.z = -j * this.size - this.size / 2.0F + zOffset;

					wall.transform.forward = Vector3.back;
					wall.transform.position = position;
				}

				if((cell.walls & Cell.Wall.East) == Cell.Wall.East) {
					var wall = this.CreateWall();
					wall.name = "East";
					wall.SetActive(true);
					wall.transform.SetParent(parent);

					Vector3 position = Vector3.zero;

					position.x = i * this.size + this.size / 2.0F + xOffset;
					position.z = -j * this.size + zOffset;

					wall.transform.forward = Vector3.right;
					wall.transform.position = position;
				}

				if((cell.walls & Cell.Wall.West) == Cell.Wall.West) {
					var wall = this.CreateWall();
					wall.name = "West";
					wall.SetActive(true);
					wall.transform.SetParent(parent);

					Vector3 position = Vector3.zero;

					position.x = i * this.size - this.size / 2.0F + xOffset;
					position.z = -j * this.size + zOffset;

					wall.transform.forward = Vector3.left;
					wall.transform.position = position;
				}
			}
		}

		this.mazeParent.GetComponent<MeshCombine>().AdvancedMerge();
		this.mazeParent.GetComponent<MeshCollider>().sharedMesh = this.mazeParent.GetComponent<MeshFilter>().sharedMesh;

		//var center = Vector3.zero;
		//center.x = -this.generator.Width * this.size / 2.0F;
		//center.z = +this.generator.Height * this.size / 2.0F;

		//this.mazeParent.localPosition = center;

		if(this.pellets == null || this.pellets.Length == 0) this.pellets = new GameObject[10];

		for(int i = 0; i < this.pellets.Length; ++i) {
			if(this.pellets[i]) {
				DestroyImmediate(this.pellets[i]);
			}
		}

		for(int i = 0; i < 10; ++i) {
			if(Random.Range(0, 100) >= 50) {
				this.virus.gameObject.SetActive(false);

				var virus = Instantiate(this.virus.gameObject).GetComponent<Virus>();

				virus.transform.SetParent(this.mazeParent);

				var position = Vector3.zero;

				position.x = Random.Range(0, width) * this.size + xOffset;
				position.y = 3.0F;
				position.z = -Random.Range(0, height) * this.size + zOffset;

				virus.transform.localPosition = position;
				virus.gameObject.SetActive(true);

				this.virus.gameObject.SetActive(true);
			}
		}

		for(int i = 0; i < this.pellets.Length; ++i) {
			this.pellets[i] = Instantiate(this.pellet, Vector3.zero, Quaternion.identity);

			this.pellets[i].transform.SetParent(this.mazeParent);

			var position = Vector3.zero;
			position.y = 3.0F;

			position.x = Random.Range(0, width) * this.size + xOffset;
			position.z = -Random.Range(0, height) * this.size + zOffset;

			this.pellets[i].transform.localPosition = position;
		}
	}

	public void Generate() {
		this.mazeParent.localPosition = Vector3.zero;

		this.generator.Width = this.width;
		this.generator.Height = this.height;

		this.maze = this.generator.Generate(new DFSB(System.DateTime.Now.Second), 0, 0);

		this.Regenerate();
	}
}
