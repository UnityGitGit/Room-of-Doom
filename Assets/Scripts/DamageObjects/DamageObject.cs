﻿using UnityEngine;

public class DamageObject : MonoBehaviour {

	private ParticleSystem destroyVFX;

	private bool canDoDamage = true;
	public bool destroyOnCollision;
	public bool multiDamage = false;

	public int damage;

	private void Start(){
		destroyVFX = transform.GetComponentInChildren<ParticleSystem>();
	}

	private void OnCollisionEnter2D(Collision2D coll){
		Collider2D other = coll.collider;
		TryDamageCollider (other);

		if (destroyOnCollision) {
			DoImpact ();
		}
	}

	private void OnTriggerStay2D(Collider2D other){
		TryDamageCollider (other);
	}

	private void TryDamageCollider(Collider2D other){
		if (other.tag == "Player" && (canDoDamage || multiDamage)) {
			CharacterCombat combatScript = other.GetComponent<CharacterCombat> ();
            Debug.Log("sadsd");
			if (combatScript.ValidTarget ()) {
                Debug.Log("iopio");
				combatScript.ApplyDamage (damage);
				DoImpact ();
			}
		}
	}

	private void DoImpact(){
		canDoDamage = false;
		if (destroyVFX != null) {
			destroyVFX.Play ();
			destroyVFX.transform.SetParent (null);
			Destroy (destroyVFX.gameObject, 1.5f);
		}

        if (multiDamage == false){
		    Destroy (gameObject);			

        }
	}
}
