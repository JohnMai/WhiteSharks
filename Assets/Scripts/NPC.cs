using UnityEngine;
using System.Collections;

public class NPC : CaseElement {

	enum AnimationState //enum to avoid magic numbers in the animation ArrayList
	{
		idle=0,
		conversation
	};
	
	public GameObject conversationObj, playerObj;
	public BoxCollider2D box;

	//NPC specific data fields
	public Category weaponProficiency;	//What kinds of weapons is the NPC skilled with
	public bool highClass;				//Does the NPC belong to the higher class society (top floors) or not?
	public ArrayList alibi;				//A set of info that represents an alibi, requires another npc, location
	public ArrayList animations;		//An array list of sprites representing the animation

	//enable conversation object if left mouse button is clicked.
	public void OnMouseDown(){
		if (Input.GetMouseButton (0)) {
				conversationObj.renderer.enabled = true;
				conversationObj.collider2D.enabled = true;

			//Check if enum casting works
			/*int test = (int)weaponProficiency;
			string test2 = weaponProficiency.ToString ();
			print(test);
			print(test2);*/
		}
	}
	//switch the displaying order of the npc. 
	void Update () {
		if (transform.position.y < playerObj.transform.position.y) {
			renderer.sortingLayerName= "foreground";
			renderer.sortingOrder = 2;
			box.isTrigger = true;
		}
		else{
			renderer.sortingLayerName= "middleground";
			box.isTrigger = false;
		}
	}
}
