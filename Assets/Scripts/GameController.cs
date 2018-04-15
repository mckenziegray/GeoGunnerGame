﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	/*enum HazardType { SquareGray, SquareYellow, SquareOrange, SquareRed, SquareBlack, 
		CircleGray, CircleYellow, CircleOrange, CircleRed, CircleBlack,
		TriangleGray, TriangleYellow, TriangleOrange, TriangleRed, TriangleBlack,
		RhombusGray, RhombusYellow, RhombusOrange, RhombusRed, RhombusBlack }*/
	enum HazardType { Square, Circle, Triangle, Rhombus }

	private const int NUM_HAZARD_LEVELS = 5;
	private GameObject[][] hazards;
	public GameObject[] hazardsLevel1, hazardsLevel2, hazardsLevel3, hazardsLevel4, hazardsLevel5;
	public Vector2 spawnPosition;
	public float startWait;
	public float spawnWait;
	public int[] waveSizes;

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
		score = 0;
		updateScoreText ();

		shieldToggle = GameObject.FindWithTag ("Player").GetComponent<ToggleDisplay> ();
		shieldToggle.hide ();

		playerHP = GameObject.FindWithTag ("Player").GetComponent<HitPoints> ();
		createHPBar ();

		hazards = new GameObject[][] { hazardsLevel1, hazardsLevel2, hazardsLevel3, hazardsLevel4, hazardsLevel5 };

		StartCoroutine (spawnHazards());
	}
	
	// Update is called once per frame
	void Update () {
		
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
		while (true) {
			yield return new WaitForSeconds (startWait);
			yield return StartCoroutine (wave1());
		}
	}

	IEnumerator wave1()
	{
		for (int i = 0; i < waveSizes [0]; i++) {
			Vector2 spawnLocation = new Vector2 (spawnPosition.x, Random.Range (-spawnPosition.y, spawnPosition.y));

			int r = Random.Range (0, 10000);
			if (r < powerupSpawnPercent * 100)
				Instantiate (powerups[r % powerups.Length], spawnLocation, Quaternion.identity);
			else {
				r = Random.Range (0, 10000);
				int hazardType;
				if (r < 8400)
					hazardType = (int)HazardType.Square;
				else
					hazardType = (int)HazardType.Circle;

				int hazardLevel;
				r = Random.Range (0, 10000);
				if (r < 8400)
					hazardLevel = 0;
				else if (r < 9985)
					hazardLevel = 1;
				else
					hazardLevel = 2;

				Instantiate (hazards [hazardLevel][hazardType], spawnLocation, Quaternion.identity);
				yield return new WaitForSeconds (spawnWait);
			}
		}
	}
}
