﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//can be used to heal a worm
public class Healer : MonoBehaviour {

	public int healCount = 1;
	public float healDuration = 3f;

	private CharacterCombat combatScript;
	public event SimpleDelegate onHeal;

	private void Start(){
		combatScript = GetComponent<CharacterCombat> ();
	}

	public void Heal(int newMaxHP){	
		healCount--;
		combatScript.MakeImmune (healDuration);
		combatScript.PrepareRevive (newMaxHP);
		StartCoroutine (HealOverTime ());
		if (onHeal != null) {
			onHeal ();
		}
	}

	private IEnumerator HealOverTime(){
		float t = 0f;
		while (t < 1f) {
			t += Time.deltaTime / healDuration;
			t = Mathf.Clamp01 (t);
			combatScript.HealUp (t);
			yield return null;
		}
			
		if (healCount == 0) {
			Destroy (this);
		}
	}
}
