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
		selected = 0;
		select (0);

		rSlider.onValueChanged.AddListener (delegate {
			changeRed ();
		});
		gSlider.onValueChanged.AddListener (delegate {
			changeGreen ();
		});
		bSlider.onValueChanged.AddListener (delegate {
			changeBlue ();
		});
	}

	public void select(int index)
	{
		if (index < players.Length && index < borders.Length) {
			borders [selected].enabled = false;
			selected = index;
			borders [index].enabled = true;
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
}
