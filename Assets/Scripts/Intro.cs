﻿using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	SpriteRenderer r;
	Color a;
	bool started = false;
	bool theEnd = false;
	bool change = false;
	bool fadeOut = true;
	bool start = true;
	bool fadeInDone = false;
	bool playingSound = false;
	public Sprite next;
	// Use this for initialization
	void Start () {
		Texture2D tex = (Texture2D)Resources.Load ("FrankSprite");
		DialogueGUI dGUI = GameManager.Instance.GetComponent<DialogueGUI> ();
		dGUI.setTargetTex (tex);
		r = GetComponent<SpriteRenderer> ();
		a = r.color;
		a.a = 0;
		r.color = a;
	}
	
	// Update is called once per frame
	void Update () {
		if (r.color.a < 1 && start) {
						a.a += 0.01f;
						r.color = a;
				} else if (!started) {
			start = false;
						started = true;
						initDialogue(0);
		}

		if (GameManager.dialogueJustFinished) {
			GameManager.dialogueJustFinished = false;
			if (!theEnd){
				change = true;

			} else {
				StartCoroutine("wait");

			}
		}

		if (change) {
			Debug.Log ("In Change " + r.color.a + " " + fadeOut);
			if (r.color.a >= 0 && fadeOut) {
					Debug.Log ("Fading out");
					a.a -= 0.016f;
					r.color = a;
			} 
			else {
				fadeOut = false;
				r.sprite = 	next;
					if (r.color.a < 1) {
					Debug.Log ("a " + a.a);
					Debug.Log ("r " + r.color.a);
					a.a += 0.01f;
					r.color = a;
					} else {
					fadeInDone = true;
				}
				if(!playingSound) {
					Debug.Log("play heli");
					SoundManager.Instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/Helicopter"), SoundManager.SoundType.Sfx);
					playingSound = true;
				}	
			}
			if (fadeInDone){
				r.color = a;
				StartCoroutine("test");
				theEnd = true;	
				change = false;
			}
		}
	}

	IEnumerator test(){
		yield return new WaitForSeconds (1.5f);
		initDialogue (6);
	}

	IEnumerator wait(){
		SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/Fin"));
		SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/" + GameManager.episodeStartLevels[GameManager.currentEpisode]));
		//Application.LoadLevel (GameManager.episodeStartLevels[GameManager.currentEpisode]);
		yield return new WaitForSeconds (1.5f);
		GameManager.Instance.playerInScene = true;
		Application.LoadLevel("finroom");
	}

	void initDialogue(int num){
		Debug.Log ("aloha");
		Dialoguer.StartDialogue(num);
	}
	

}
