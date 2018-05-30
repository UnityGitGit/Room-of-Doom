﻿using UnityEngine;

//the player implementation for combat
public class PlayerCombat : CharacterCombat {

	private PlayerInput input;
	private PlayerBase baseScript;
	private Animator anim;

	[Header("hit check values")]
	public LayerMask enemyLM;
	public Transform weaponCheck;

	[Header("weapon values")]
	public int attackDamage;
	public float attackRange;
	public float cooldown;
	public float remainingCooldown;

	private void Start(){
		baseScript = GetComponent<PlayerBase> ();
		anim = GetComponentInChildren<Animator> ();
		input = GetComponent<PlayerInput> ();
	}

	protected override void Update () {
		base.Update ();
		if (!baseScript.canControl)
			return;

		//if (remainingCooldown == 0f && input.ButtonIsDown(PlayerInput.Button.B)) {
		//	 Attack ();
		//}
		//else if (remainingCooldown > 0f) {
		//	Cooldown ();
		//}
	}

	//activate the cooldown and try to hit someone
	private void Attack(){
		remainingCooldown = cooldown;
		Collider2D other = Physics2D.OverlapCircle(weaponCheck.position, attackRange, enemyLM);
        if (other != null) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, (other.transform.position - transform.position), attackRange * 2f, enemyLM);
			IAttackable combatScript = other.GetComponent<IAttackable> ();
			combatScript.ApplyDamage (attackDamage, hit.point, (Vector3)hit.point - transform.transform.position);
        }

		anim.SetTrigger ("attack");
	}

	private void Cooldown(){
		remainingCooldown -= Time.deltaTime;
		remainingCooldown = (remainingCooldown < 0f) ? 0f : remainingCooldown;
	}

	//draw a circle where the weapon can hit
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (weaponCheck.position, attackRange);
	}
}
