using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadSceneByIndex(int sceneIndex)
	{
		Debug.Log (String.Format("Loading scene {0}", sceneIndex));
		SceneManager.LoadScene (sceneIndex);
	}
}
