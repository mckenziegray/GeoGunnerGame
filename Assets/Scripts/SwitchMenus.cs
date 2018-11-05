using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMenus : MonoBehaviour {

	public GameObject[] MainMenuPanels;
	public GameObject[] PlayerSelectorMenuPanels;

	public void MainMenu()
	{
		foreach (GameObject o in PlayerSelectorMenuPanels) {
			o.SetActive (false);
		}
		foreach (GameObject o in MainMenuPanels) {
			o.SetActive (true);
		}
	}

	public void PlayerSelectorMenu()
	{
		foreach (GameObject o in MainMenuPanels) {
			o.SetActive (false);
		}
		foreach (GameObject o in PlayerSelectorMenuPanels) {
			o.SetActive (true);
		}
	}
}
