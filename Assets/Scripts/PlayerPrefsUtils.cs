using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerPrefsUtils : MonoBehaviour {

	public static bool AddHighScore(int score)
	{
		bool newHighScore = false;

		int numHighScores = Globals.PP_HIGH_SCORES.Length;
		List<int> highScores = new List<int>(numHighScores+1);
		for (int i = 0; i < Globals.PP_HIGH_SCORES.Length; i++) {
			highScores.Add(PlayerPrefs.GetInt (Globals.PP_HIGH_SCORES [i], 0));
		}

		if (score > highScores [0])
			newHighScore = true;

		// Add the new score, then sort in descending order, so only the top 5 will be recorded
		highScores.Add (score);
		highScores = highScores.OrderByDescending(s => s).ToList();

		for (int i = 0; i < numHighScores; i++) {
			PlayerPrefs.SetInt (Globals.PP_HIGH_SCORES [i], highScores [i]);
		}

		return newHighScore;
	}
}
