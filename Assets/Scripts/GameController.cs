using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour {

	/*enum HazardType { SquareGray, SquareYellow, SquareOrange, SquareRed, SquareBlack, 
		CircleGray, CircleYellow, CircleOrange, CircleRed, CircleBlack,
		TriangleGray, TriangleYellow, TriangleOrange, TriangleRed, TriangleBlack,
		RhombusGray, RhombusYellow, RhombusOrange, RhombusRed, RhombusBlack }*/
	enum HazardType { Square, Circle, Triangle, Rhombus }

	public Text mainText;
	public GameObject[] players;
	private GameObject player;

	private const int NUM_HAZARD_LEVELS = 5;
	private GameObject[][] hazards;
	public GameObject[] hazardsLevel1, hazardsLevel2, hazardsLevel3, hazardsLevel4, hazardsLevel5;
	public Vector2 hazardSpawnPosition;
	public float startWait;
	public float spawnWait;
	public int[] waveSizes;
	public int numberOfWaves;
	private float[] hazardPercentages;
	private float[] hazardTypeChances;
	private float[] hazardLevelChances;
	public float waveTextDisplayTime;

	public float powerupSpawnPercent;
	public GameObject[] powerups;

	public GameObject HPCellFull;
	public GameObject HPCellEmpty;
	public Vector2 HPBarPosition;
	public float HPCellSpacing;
	private GameObject[] HPCells;
	private int lastFullCell;
	private HitPoints playerHP;

	public GameObject ShieldCell;
	public int maxShields;
	private GameObject[] shieldCells;
	private int numShields;
	private ToggleDisplay shieldToggle;

	private int score;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		foreach (GameObject p in players) {
			p.SetActive (false);
		}

		score = 0;
		updateScoreText ();

		player = players[PlayerPrefs.GetInt ("player", 0)];
		player.GetComponent<SpriteRenderer> ().color = new Color (
			PlayerPrefs.GetFloat ("player-r"),
			PlayerPrefs.GetFloat ("player-g"),
			PlayerPrefs.GetFloat ("player-b")
		);

		player.SetActive (true);

		shieldToggle = player.GetComponent<ToggleDisplay> ();
		shieldToggle.hide ();

		playerHP = player.GetComponent<HitPoints> ();
		createHPBar ();

		hazards = new GameObject[][] { hazardsLevel1, hazardsLevel2, hazardsLevel3, hazardsLevel4, hazardsLevel5 };
		hazardPercentages = new float[] { 0.15f, 2.35f, 13.5f, 34f, 34f, 13.5f, 2.35f, 0.15f };


		StartCoroutine (spawnHazards());
	}

	private void createHPBar()
	{
		HPCells = new GameObject[playerHP.maxHP];
		shieldCells = new GameObject[maxShields];
		HPCellFull.GetComponent<SpriteRenderer> ().color = Color.green;
		HPCellEmpty.GetComponent<SpriteRenderer> ().color = new Color (0.9f, 1f, 0.9f, 1f); //Pale green

		Vector2 cellLocation; 
		for (lastFullCell = 0; lastFullCell < HPCells.Length; lastFullCell++)
		{
			cellLocation = new Vector2 (HPBarPosition.x + (HPCellSpacing * lastFullCell), HPBarPosition.y);
			HPCells[lastFullCell] = Instantiate (HPCellFull, cellLocation, Quaternion.identity);
		}

		lastFullCell--;
	}

	public void damagePlayer(int damageAmt) {
		for (int i = 0; i < damageAmt; i++) {
			if (numShields > 0) {
				Destroy (shieldCells [--numShields].gameObject);
				if (numShields == 0)
					shieldToggle.hide ();
			}
			else {
				playerHP.Damage (1);
				//replace full cell with empty cell
				Vector2 cellLocation = new Vector2 (HPBarPosition.x + (HPCellSpacing * lastFullCell), HPBarPosition.y);
				Destroy (HPCells [lastFullCell].gameObject);
				HPCells[lastFullCell] = Instantiate (HPCellEmpty, cellLocation, Quaternion.identity);
				lastFullCell--;
			}

			if (lastFullCell < 0)
				break;
		}
	}

	public void healPlayer(int healAmt) {
		for (int i = 0; i < healAmt; i++) {
			if (lastFullCell == playerHP.maxHP-1)
				break;
			playerHP.Heal (1);
			Vector2 cellLocation = new Vector2 (HPBarPosition.x + (HPCellSpacing * (++lastFullCell)), HPBarPosition.y);
			Destroy (HPCells [lastFullCell].gameObject);
			HPCells[lastFullCell] = Instantiate (HPCellFull, cellLocation, Quaternion.identity);
		}
	}

	public void giveShield()
	{
		if (numShields == 0)
			shieldToggle.show ();
		if (numShields <= maxShields) {
			Vector2 cellLocation = new Vector2 (HPBarPosition.x + (HPCellSpacing * (HPCells.Length + numShields)), HPBarPosition.y);
			shieldCells [numShields] = Instantiate (ShieldCell, cellLocation, Quaternion.identity);
			numShields++;
		}
	}

	public void addScore(int addedScore)
	{
		score += addedScore;
		updateScoreText ();
	}

	private void updateScoreText() {
		scoreText.text = score.ToString();
	}

	IEnumerator spawnHazards()
	{
		hazardTypeChances = new float[] { 
			(100f - hazardPercentages [0] - hazardPercentages [1]) * 100, 
			(hazardPercentages [1] + hazardPercentages [0]) * 100,
			0f,
			0f
		};

		hazardLevelChances = new float[] {
			(100f - hazardPercentages [0] - hazardPercentages [1]) * 100, 
			(hazardPercentages [0] + hazardPercentages [1]) * 100,
			0f, 
			0f, 
			0f
		};

		for (int i = 1; i <= numberOfWaves; i++) {
			//Increase chances of spawning more powerful hazards
			for (int j = 0; j + 1 < hazardLevelChances.Length; j++) {
				int mod = 6 + (j * 2) - i;
				if (mod >= 0 && mod < hazardPercentages.Length) {
					hazardLevelChances [j] -= hazardPercentages [mod] * 100;
					hazardLevelChances [j + 1] += hazardPercentages [mod] * 100;
				}
			}

			for (int j = 0; j + 1 < hazardTypeChances.Length; j++) {
				int mod = 6 + (j * 3) - i;
				if (mod >= 0 && mod < hazardPercentages.Length) {
					hazardTypeChances [j] -= hazardPercentages [mod] * 100;
					hazardTypeChances [j + 1] += hazardPercentages [mod] * 100;
				}
			}

			/*Debug.Log ("Colors:");
			foreach (float j in hazardLevelChances) {
				Debug.Log (j);
			}
			Debug.Log ("Shapes:");
			foreach (float j in hazardTypeChances) {
				Debug.Log (j);
			}*/

			yield return new WaitForSeconds (startWait);
			mainText.text = "Wave " + i;
			yield return new WaitForSeconds (waveTextDisplayTime);
			mainText.text = "";
			yield return StartCoroutine (spawnWave ());
		}
	}

	IEnumerator spawnWave()
	{
		for (int i = 0; i < waveSizes [0]; i++) {
			Vector2 spawnLocation = new Vector2 (hazardSpawnPosition.x, UnityEngine.Random.Range (-hazardSpawnPosition.y, hazardSpawnPosition.y));

			int r = UnityEngine.Random.Range (0, 10000);
			if (r < powerupSpawnPercent * 100)
				Instantiate (powerups[r % powerups.Length], spawnLocation, Quaternion.identity);
			else {
				//Determine shape
				r = UnityEngine.Random.Range (0, 10000);
				Debug.Log ("Shape roll: " + r);
				int hazardType;
				if (r < hazardTypeChances [0]) {
					hazardType = (int)HazardType.Square;
					Debug.Log ("Square");
				}
				else if (r < hazardTypeChances[0] + hazardTypeChances[1]) {
					hazardType = (int)HazardType.Circle;
					Debug.Log ("Circle");
				}
				else if (r < hazardTypeChances[0] + hazardTypeChances[1] + hazardTypeChances[2]) {
					hazardType = (int)HazardType.Triangle;
					Debug.Log ("Triangle");
				}
				else {
					hazardType = (int)HazardType.Rhombus;
					Debug.Log ("Rhumbus");
				}

				//Determine color
				r = UnityEngine.Random.Range (0, 10000);
				Debug.Log ("Color roll: " + r);
				int hazardLevel;
				if (r < hazardLevelChances [0]) {
					hazardLevel = 0;
					Debug.Log ("Gray");
				}
				else if (r < hazardLevelChances [0] + hazardLevelChances [1]) {
					hazardLevel = 1;
					Debug.Log ("Yellow");
				}
				else if (r < hazardLevelChances [0] + hazardLevelChances [1] 
					+ hazardLevelChances [2]) {
					hazardLevel = 2;
					Debug.Log ("Orange");
				}
				else if (r < hazardLevelChances [0] + hazardLevelChances [1] 
					+ hazardLevelChances [2] + hazardLevelChances [3]) {
					hazardLevel = 3;
					Debug.Log ("Red");
				}
				else {
					hazardLevel = 4;
					Debug.Log ("Black");
				}

				Instantiate (hazards [hazardLevel][hazardType], spawnLocation, Quaternion.identity);
				yield return new WaitForSeconds (spawnWait);
			}
		}
	}
}
