using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin = -6, xMax = 6, zMin = -4, zMax = 4;
}

public class PlayerController : MonoBehaviour {

	public GameObject bolt, shotSpawn;
	private float speed = 5;
	public float tilt;
	public float nextFire, fireRate;
	public Boundary boundary;

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3
		(
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
		);
		//rigidbody.AddForce(movement);

		rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}

	void Update()
	{
		if (Input.GetButton("Jump") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(bolt, shotSpawn.transform.position, shotSpawn.transform.rotation);
		}
	}
}
