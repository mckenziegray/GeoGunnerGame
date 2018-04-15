using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public Boundary boundary;
	public float secondsBetweenShots;
	private float nextShot;

	public GameObject missile;
	public Transform missileSpawn;

	void Start () 
	{
		missile.GetComponent<SpriteRenderer> ().color = this.GetComponent<SpriteRenderer> ().color;
		nextShot = 0;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton ("Fire1") && Time.time > nextShot) {
			nextShot = Time.time + secondsBetweenShots;
			Instantiate (missile, missileSpawn.position, missileSpawn.rotation);
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		GetComponent<Rigidbody2D> ().velocity = movement * speed;

		GetComponent<Rigidbody2D> ().position = new Vector2 (
			Mathf.Clamp (GetComponent<Rigidbody2D> ().position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp (GetComponent<Rigidbody2D> ().position.y, boundary.yMin, boundary.yMax)
		);
	}
}
