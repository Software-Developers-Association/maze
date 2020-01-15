using UnityEngine;

[System.Serializable]
public abstract class Behaviour {
	protected readonly GameObject gameObject;

	public Behaviour(GameObject gameObject) {
		this.gameObject = gameObject;
	}

	public abstract bool State(Process process);
}

[System.Serializable]
public class Patrol : Behaviour {
	public LayerMask wall;

	private float nextPatrolCheck = 0.0F;
	private Direction direction = Direction.Horizontal;
	private bool isPositiveDirection = true;

	public Patrol(LayerMask wall, GameObject gameObject) : base(gameObject) {
		this.wall = wall;
		this.Speed = 20.0F;

		this.direction = (Direction)Random.Range(0, 2);

		switch(direction) {
			case Direction.Horizontal:
				{
					bool canMove = false;

					// Check if we can move left or right...
					if(!Physics.Raycast(this.gameObject.transform.position, Vector3.left, 10F, this.wall)) {
						canMove |= true;
					}

					if(!Physics.Raycast(this.gameObject.transform.position, Vector3.right, 10F, this.wall)) {
						canMove |= true;
					}

					if(canMove == false) {
						direction = Direction.Vertical;
					}
				}
				break;
			case Direction.Vertical:
				{
					bool canMove = false;

					// Check if we can move left or right...
					if(!Physics.Raycast(this.gameObject.transform.position, Vector3.forward, 10F, this.wall)) {
						canMove |= true;
					}

					if(!Physics.Raycast(this.gameObject.transform.position, Vector3.back, 10F, this.wall)) {
						canMove |= true;
					}

					if(canMove == false) {
						direction = Direction.Horizontal;
					}
				}
				break;
		}
	}

	public float Speed { get; set; }

	public override bool State(Process process) {
		switch(process) {
			case Process.Update:
				//if(Time.time >= this.nextPatrolCheck) {
				//	this.nextPatrolCheck = Time.time + 2.0F;

				//	// Check if we can move left or right...
				//	if(!Physics.Raycast(this.gameObject.transform.position, Vector3.left, 5.5F, this.wall)) {
				//		direction = Direction.Horizontal;

				//		break;
				//	}

				//	if(!Physics.Raycast(this.gameObject.transform.position, Vector3.right, 5.5F, this.wall)) {
				//		direction = Direction.Horizontal;
				//	}

				//	if(!Physics.Raycast(this.gameObject.transform.position, Vector3.forward, 5.5F, this.wall)) {
				//		direction = Direction.Vertical;

				//		break;
				//	}

				//	if(!Physics.Raycast(this.gameObject.transform.position, Vector3.back, 5.5F, this.wall)) {
				//		direction = Direction.Vertical;
				//	}
				//}

				switch(this.direction) {
					case Direction.Horizontal: this.Horizontal(); break;
					case Direction.Vertical: this.Vertical(); break;
				}
			break;
		}

		return true;
	}

	protected virtual void Horizontal() {
		if(this.isPositiveDirection) {
			if(Physics.Raycast(this.gameObject.transform.position, Vector3.right, 5.5F, this.wall)) {
				this.isPositiveDirection = false;
			}
		} else {
			if(Physics.Raycast(this.gameObject.transform.position, Vector3.left, 3.5F, this.wall)) {
				this.isPositiveDirection = true;
			}
		}

		var dir = Vector3.zero;

		if(this.isPositiveDirection) {
			if(Physics.Raycast(this.gameObject.transform.position, Vector3.right, 3.5F, this.wall)) {
				dir.x = 0.0F;
			} else {
				dir.x = 1.0F;
			}
		} else {
			if(Physics.Raycast(this.gameObject.transform.position, Vector3.left, 3.5F, this.wall)) {
				dir.x = 0.0F;
			} else {
				dir.x = -1.0F;
			}
		}

		dir.x *= this.Speed * Time.deltaTime;

		this.gameObject.transform.position += dir;
	}

	protected virtual void Vertical() {
		if(this.isPositiveDirection) {
			if(Physics.Raycast(this.gameObject.transform.position, Vector3.forward, 3.5F, this.wall)) {
				this.isPositiveDirection = false;
			}
		} else {
			if(Physics.Raycast(this.gameObject.transform.position, Vector3.back, 3.5F, this.wall)) {
				this.isPositiveDirection = true;
			}
		}

		var dir = Vector3.zero;

		if(this.isPositiveDirection) {
			if(Physics.Raycast(this.gameObject.transform.position, Vector3.forward, 3.5F, this.wall)) {
				dir.z = 0.0F;
			} else {
				dir.z = 1.0F;
			}
		} else {
			if(Physics.Raycast(this.gameObject.transform.position, Vector3.back, 3.5F, this.wall)) {
				dir.z = 0.0F;
			} else {
				dir.z = -1.0F;
			}
		}

		dir.z *= this.Speed * Time.deltaTime;

		this.gameObject.transform.position += dir;
	}

	public enum Direction {
		Horizontal = 0,
		Vertical
	}
}