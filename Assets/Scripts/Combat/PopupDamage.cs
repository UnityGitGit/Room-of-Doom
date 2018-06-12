﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupDamage : MonoBehaviour {

    private Animator ani;
	public Text damageText;
    private Image texture;

	private void OnEnable(){
        ani = transform.GetChild(0).GetComponent<Animator>();
        AnimatorClipInfo[] clipInfos = ani.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfos[0].clip.length);
		damageText = transform.GetChild(0).GetComponent<Text>();
        texture = transform.GetChild(1).GetComponent<Image>();

		Debug.Log ("damage text: " + damageText);
    }

    public void SetText(string text){
        damageText.text = text;

    }

    public void SetTexture(Sprite t){
        texture.sprite = t;
    }

}
