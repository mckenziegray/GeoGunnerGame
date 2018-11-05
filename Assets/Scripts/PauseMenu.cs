using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenuPanel;
	private bool paused;

	// Use this for initialization
	void Start () {
		pauseMenuPanel.SetActive (false);
		paused = false;
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
		Time.timeScale = 0; // Stop time

		pauseMenuPanel.SetActive (true);
		paused = true;
	}

	public void Unpause()
	{
		pauseMenuPanel.SetActive (false);
		paused = false;

		Time.timeScale = 1; // Resume time
	}
}
