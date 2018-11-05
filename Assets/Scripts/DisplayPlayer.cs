using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayer : MonoBehaviour {
	public GameObject[] playerShapes;
	public int defaultShape;
	public Color defaultColor;

	// Use this for initialization
	void Start () {
		foreach (GameObject shape in playerShapes) {
			shape.SetActive (false);
		}
			
		int index = PlayerPrefs.GetInt("player", 0);

		playerShapes [index].GetComponent<Image> ().color = new Color (
			PlayerPrefs.GetFloat ("player-r", Random.Range (0.0f, 1.0f)), 
			PlayerPrefs.GetFloat ("player-g", Random.Range (0.0f, 1.0f)),
			PlayerPrefs.GetFloat ("player-b", Random.Range (0.0f, 1.0f))
		);

		playerShapes [index].SetActive (true);
	}
}
