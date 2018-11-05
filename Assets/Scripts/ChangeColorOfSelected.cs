using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorOfSelected : MonoBehaviour {

	public Selectable[] players;
	public Image[] borders;
	private int selected;

	public Slider rSlider;
	public Slider gSlider;
	public Slider bSlider;

	void Start() {
		foreach (Image border in borders)
			border.enabled = false;

		selected = PlayerPrefs.GetInt("player", 0);
		select (selected);

		rSlider.onValueChanged.AddListener (delegate {
			changeRed ();
		});
		gSlider.onValueChanged.AddListener (delegate {
			changeGreen ();
		});
		bSlider.onValueChanged.AddListener (delegate {
			changeBlue ();
		});

		rSlider.value = PlayerPrefs.GetFloat ("player-r", 0.0f);
		gSlider.value = PlayerPrefs.GetFloat ("player-g", 0.0f);
		bSlider.value = PlayerPrefs.GetFloat ("player-b", 0.0f);
	}

	public void select(int index)
	{
		if (index < players.Length && index < borders.Length) {
			borders [selected].enabled = false;
			selected = index;
			borders [index].enabled = true;

			rSlider.value = players [selected].GetComponent<Image> ().color.r;
			gSlider.value = players [selected].GetComponent<Image> ().color.g;
			bSlider.value = players [selected].GetComponent<Image> ().color.b;
		}
		else
			Debug.Log (index + " outside index range");
	}

	private void changeRed()
	{
		players[selected].GetComponent<ChangeColor> ().setRed (rSlider.value);
	}

	private void changeGreen()
	{
		players[selected].GetComponent<ChangeColor> ().setGreen (gSlider.value);
	}

	private void changeBlue()
	{
		players[selected].GetComponent<ChangeColor> ().setBlue (bSlider.value);
	}

	public void ConfirmSelection()
	{
		PlayerPrefs.SetInt("player", selected);
		PlayerPrefs.SetFloat("player-r", players[selected].GetComponent<Image> ().color.r);
		PlayerPrefs.SetFloat("player-g", players[selected].GetComponent<Image> ().color.g);
		PlayerPrefs.SetFloat("player-b", players[selected].GetComponent<Image> ().color.b);
	}
}
