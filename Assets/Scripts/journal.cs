﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class journal : MonoBehaviour {

	private static journal instance;
	//private static int MAX_NPC = 3;
	private static journal j;

	public static journal Instance {
		get {
			if (instance == null) {
				Debug.Log("JOURNAL: Instance null, creating new Journal");
				instance = new GameObject("journal").AddComponent<journal>();
			}
			return instance;
		}
	}

	private List<NPC> personsOfInterest;
	private ArrayList weaponList;

	//Defaults for non-visible NPC
	public static Sprite emptyPortrait;
	private string emptyName;

	//Grab view tab buttons. Will change to use gameobject find.
	public GameObject viewTab1;
	public GameObject viewTab2;
	public GameObject viewTab3;

	//Grab two main journal activation buttons and boolean to check when in menu.
	public GameObject journalButton;
	public GameObject accusationRoomButton;
	private bool inMenu;

	//Grab buttons and textfield from view. Will change to use gameobject find. Three lists for three different types of buttons.
	private static List<GameObject> viewTabList;
	private static List<GameObject> poiButtonList;
	private static List<GameObject> objectButtonList;

	public GameObject poiButtonGrid;
	public GameObject objectButtonGrid;
	public UI2DSprite poiPortrait;

	//Buttons for accusation panel.
	private static List<GameObject> suspectButtonList;
	private static List<GameObject> weaponButtonList;
	private static List<GameObject> mapButtonList;

	public GameObject suspectGrid;

	public UILabel descriptionLabel;
	public UILabel panelNameLabel;
	public UILabel timeLabel;

	//Destroys duplicate UI Roots.
	void Awake () {
		//journalButton.transform.position = new Vector3(275, 20, 0);
		if(!j){
			j = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy (gameObject);
		}
	}
	
	void Start () {
		////----    Journal Panel Init    ----//////////////////////////////////////////////////////

		//Default name for "invisible" person of interest.
		emptyName = "?????";

		//Persons of interest list.
		personsOfInterest = GameManager.npcList;

		//Weapon list once that's ready.
		//weaponList = GameManager.weaponList;

		//Listens for tab button presses in journal and runs onClick with button clicked as parameter.
		UIEventListener.Get (viewTab1).onClick += this.onClick;
		UIEventListener.Get (viewTab2).onClick += this.onClick;
		UIEventListener.Get (viewTab3).onClick += this.onClick;

		//Listens for journal/accusation room buttons to make sure only one is active at a time.
		UIEventListener.Get (journalButton).onClick += this.journalActivationRoomToggle;
		UIEventListener.Get (accusationRoomButton).onClick += this.journalActivationRoomToggle;

		//Want to get rid of this too.
		viewTabList = new List<GameObject>();
		viewTabList.Add(viewTab1);
		viewTabList.Add(viewTab2);
		viewTabList.Add(viewTab3);

		//List of person of interest portrait buttons.
		poiButtonList = new List<GameObject>();
		objectButtonList = new List<GameObject>();

		changeView(0);
		initPoIView ();
		//initObjView ();
		StartCoroutine (UpdateTime ());

		////----    Accusation Panel Init    ----//////////////////////////////////////////////////////
		suspectButtonList = new List<GameObject>();
		initAccusationPanel();
	}

	//Single onclick function for any button in the journal.
	void onClick(GameObject button){
		if(viewTabList != null && viewTabList.Contains(button)){
			changeView (viewTabList.IndexOf(button));
			Debug.Log ("won't happen yet");
		}
		else if(poiButtonList.Contains(button)){
			changePOI(poiButtonList.IndexOf(button));
			Debug.Log ("poiButton!");
		}
		else if (objectButtonList.Contains (button)){
			changeObject(objectButtonList.IndexOf(button));
			Debug.Log ("objectbutton!");
		}
	}

	void journalActivationRoomToggle(GameObject button){
		if (button == journalButton){
			if (inMenu){
				accusationRoomButton.SetActive(true);
				inMenu = false;
			}
			else {
				accusationRoomButton.SetActive(false);
				inMenu = true;
			}
		}
		else if (button == accusationRoomButton){
			if (inMenu){
				journalButton.SetActive(true);
				inMenu = false;
			}
			else {
				journalButton.SetActive(false);
				inMenu = true;
			}
		}
	}

	//----- Button type functions
	//Changes view when view tab is clicked.
	//Will make helper function for grid/view SetActive.
	void changeView(int viewNumber){
		clearLabels ();
		switch (viewNumber) {
			case 0://PoI
				//mapView.SetActive(false);
				//poiView.SetActive(true);
				objectButtonGrid.SetActive(false);
				poiButtonGrid.SetActive(true);
				break;
			case 1://Object
				//mapView.SetActive (false);
				//poiView.SetActive(true);
				objectButtonGrid.SetActive(true);
				poiButtonGrid.SetActive(false);
				break;
			case 2://Map
				//mapView.SetActive (true);
				//poiView.SetActive(false);
				objectButtonGrid.SetActive(false);
				poiButtonGrid.SetActive(false);
				break;
		}
	}

	//Changes PoI when a PoI portrait is clicked.
	void changePOI(int poiNumber){
		//Sprint 2 change POI code.
		if (personsOfInterest [poiNumber].isVisible ()) {
			descriptionLabel.text = personsOfInterest[poiNumber].getDescription();
		}
		else {
			descriptionLabel.text = emptyName;
		}
		poiPortrait.sprite2D = personsOfInterest[poiNumber].getProfileImage();
		panelNameLabel.text = personsOfInterest [poiNumber].getElementName ();
	}

	//Changes object/weapon being viewed when portrait is clicked.
	void changeObject(int objectNumber){

	}

	//Initialize PoI view. 
	//Code for sprint 2 journal.
	public void initPoIView(){
		//Add buttons to poi button list and put them in UI event listener.
		foreach (Transform child in poiButtonGrid.transform){
			UIEventListener.Get(child.gameObject).onClick += this.onClick;
			poiButtonList.Add(child.gameObject);
		}
		//Put suspect names on poi button labels.
		for (int i = 0; i < personsOfInterest.Count; i++) {
			if(personsOfInterest[i] != null){
				poiButtonList[i].gameObject.GetComponentInChildren<UILabel>().text = personsOfInterest[i].getElementName();
			}
			else {
				poiButtonList[i].gameObject.GetComponentInChildren<UILabel>().text = emptyName;
			}
		}

		//Load first POI
		changePOI (0);
	}

	//Initialize obj view.
	public void initObjView(){
		//Add buttons to obj button list and put them in UI event listener.
		foreach (Transform child in objectButtonGrid.transform){
			UIEventListener.Get(child.gameObject).onClick += this.onClick;
			objectButtonList.Add(child.gameObject);
		}
	}

	//Clear description labels. Might rename and add obj/poi grid on/off.
	void clearLabels(){
		panelNameLabel.text = "";
		descriptionLabel.text = "";
	}

	//Keeps time.
	IEnumerator UpdateTime(){
		while (true) {
			DateTime currentTime = System.DateTime.Now;
			timeLabel.text = currentTime.ToString("HH:mm");
			timeLabel.text += "//";
			yield return new WaitForSeconds(0.2f);
		}
	}

	public void updateKnowledge(DictEntry postState){
		//A stub because i don't know what this is.
	}

	public Dictionary getKnowledge(){
		//A stub because I dont' know what this is.
		Dictionary pk = new Dictionary();
		return pk;
	}

	////----    Accusation Panel    ----////////////////////////////////////////////////////
	void initAccusationPanel(){
		Debug.Log ("init");
		foreach(Transform child in suspectGrid.transform){
			UIEventListener.Get(child.gameObject).onClick += this.accusationOnClick;
			suspectButtonList.Add (child.gameObject);
		}

		for (int i = 0; i < suspectButtonList.Count; i++){
			suspectButtonList[i].GetComponentInChildren<UILabel>().text = personsOfInterest[i].getElementName();
			suspectButtonList[i].GetComponentInChildren<UI2DSprite>().sprite2D = personsOfInterest[i].getProfileImage();
		}
	}

	void accusationOnClick(GameObject button){
		Debug.Log ("here");
	}
}
