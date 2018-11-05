using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDisplay : MonoBehaviour {

	public bool startsEnabled;
	public GameObject itemToToggle;
	private Renderer r;

	void Awake() {
		r = itemToToggle.GetComponent<Renderer> ();
		r.enabled = startsEnabled;
	}

	public void show() {
		r.enabled = true;
	}

	public void hide() {
		r.enabled = false;
	}
}
