﻿using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {

	public clickableID diaNum;
	public int offset;

	//Mouse icon information
	private string mouseOverIcon = "Examine_Icon";	
	private string defaultIcon = "Walk_Icon";		//The standard mouse icon when not hovering over an object
	private playerScript player;
	public bool clickedOnSomething = false;

	public float maxDist = 1f;

	public void OnMouseEnter(){
		if (player != null)
			if (player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}

	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (defaultIcon);
	}

	public void OnMouseDown(){
		if (Input.GetMouseButton (0)) 
			if (player.canWalk == true)	
				clickedOnSomething = true;

		
	}

	void Start(){
		player = (playerScript) FindObjectOfType(typeof(playerScript));
		offset = GameManager.Instance.offset;
		if (maxDist < GameManager.Instance.maxDist)
			maxDist = GameManager.Instance.maxDist;
	}

	void Update(){

		if (Input.GetMouseButton (0)) {
			
			RaycastHit hit = new RaycastHit ();        
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit))
			if (hit.collider.gameObject != this.gameObject)
					clickedOnSomething = false;
		}
		
		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

		if (player.canWalk == true)
		if (Vector3.Distance(player.transform.position, transform.position) <= maxDist && clickedOnSomething){
			Dialoguer.StartDialogue((int)diaNum + offset);
			player.setTarget(new Vector2(player.transform.position.x, player.transform.position.y));
			player.canWalk = false;
			player.anim.SetFloat("distance", 0f);
			player.anim.SetBool("walking", false);
			OnMouseExit();
			clickedOnSomething = false;
		}
		
	}

}
