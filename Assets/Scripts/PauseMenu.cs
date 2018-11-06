using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenuPanel;
	private bool paused;
	private GameObject player;

	// Use this for initialization
	void Start () {
		pauseMenuPanel.SetActive (false);
		paused = false;

		List<GameObject> playerObjects = new List<GameObject> ((GameObject.FindGameObjectsWithTag ("Player")));
		player = playerObjects.FirstOrDefault (p => p.activeInHierarchy);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Cancel")) {
			if (paused)
				Unpause ();
			else
				Pause ();
		}
	}

	public void Pause()
	{
		// Freeze time
		// Input still works while time is frozen, unless it's being read in FixedUpdate()
		Time.timeScale = 0;

		// Any scripts that don't rely on time to work need to be disabled
		player.GetComponent<PlayerController>().enabled = false;

		pauseMenuPanel.SetActive (true);
		paused = true;
	}

	public void Unpause()
	{
		pauseMenuPanel.SetActive (false);
		paused = false;

		// Re-enable any scripts that were disabled
		player.GetComponent<PlayerController>().enabled = true;

		Time.timeScale = 1; // Unfreeze time
	}
}
