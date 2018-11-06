using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public float speed;
	public float horizontalPosition;
	public float minY;
	public float maxY;
	public float secondsBetweenShots;
	private float nextShot;

	public GameObject missile;
	public Transform missileSpawn;

	void Start () {
		nextShot = 0;
		StartCoroutine (Move ());
		StartCoroutine (Shoot ());
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator Move()
	{
		GetComponent<Rigidbody2D> ().velocity = transform.right * speed;

		// Wait for x position to be set, then stop horizontal movement
		while (this.transform.position.x < horizontalPosition) {}
		GetComponent<Rigidbody2D> ().velocity = transform.right * 0;

		while (true) {
			float curY = this.transform.position.y;

			// Move to a random y position
			float nextY = Random.Range (minY, maxY);
			if (nextY > curY) {
				GetComponent<Rigidbody2D> ().velocity = transform.up * speed;
				while (nextY > curY) {}
			} else {
				GetComponent<Rigidbody2D> ().velocity = -transform.up * speed;
				while (nextY < curY) {}
			}
		}
	}

	IEnumerator Shoot()
	{
		while (true)
		{
			if (Time.time > nextShot) {
				nextShot = Time.time + secondsBetweenShots;
				Instantiate (missile, missileSpawn.position, missileSpawn.rotation);
			}
		}
	}
}
