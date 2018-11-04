using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour {

	public int maxHP;
	private int currentHP;

	// Use this for initialization
	void Start () {
		this.currentHP = maxHP;
	}
	
	public void Damage (int damage)
	{
		this.currentHP -= damage;
		if (this.currentHP <= 0) {
			this.currentHP = 0;
			this.Die ();
		}
	}
	public void Heal(int healing)
	{
		this.currentHP += healing;
		if (this.currentHP > maxHP)
			this.currentHP = maxHP;
	}

	private void Die ()
	{
	}
}
