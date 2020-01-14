using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour {
	[SerializeField]
	private float speed = 50.0F;
	[SerializeField]
	private LayerMask wallMask;
	private Rigidbody rig;

	private void Awake() {
		this.rig = this.GetComponent<Rigidbody>();
	}

	private void Update() {
		var direction = Vector3.zero;

		direction.x = Input.GetAxisRaw("Horizontal");
		direction.z = Input.GetAxisRaw("Vertical");
		//direction.Normalize();

		Ray ray = new Ray(this.transform.position, Vector3.forward * direction.z);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 5.5F, this.wallMask)) {
			direction.z = 0.0F;
		}

		ray.direction = Vector3.right * direction.x;

		if(Physics.Raycast(ray, out hit, 5.5F, this.wallMask)) {
			direction.x = 0.0F;
		}

		this.rig.position += direction * this.speed * Time.deltaTime;
	}
}