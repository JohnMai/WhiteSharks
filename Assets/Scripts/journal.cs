using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class journal : MonoBehaviour {

	private static journal instance;
	private static int MAX_NPC = 3;

	public static journal Instance {
		get {
			if (instance == null) {
				Debug.Log("JOURNAL: Instance null, creating new Journal");
				instance = new GameObject("journal").AddComponent<journal>();
			}
			return instance;
		}
	}

	public List<NPC> personsOfInterest;

	//Defaults for non-visible NPC
	public static Sprite emptyPortrait;
	private string emptyName;

	//Grab view tab buttons. Will change to use gameobject find.
	public GameObject viewTab1;
	public GameObject viewTab2;
	public GameObject viewTab3;

	//Grab buttons and textfield from view. Will change to use gameobject find. Three lists for three different types of buttons.
	private static List<GameObject> viewTabList;
	private static List<GameObject> poiButtonList;
	private static List<GameObject> objectButtonList;
	public GameObject poiButton1;
	public GameObject poiButton2;
	public GameObject poiButton3;
	public UILabel nameLabel;
	public UILabel descriptionLabel;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(GameObject.Find("UI Root"));
		//Default name for "invisible" person of interest.
		emptyName = "?????";

		//Person of interest list.
		personsOfInterest = GameManager.npcList;

		//Listens for tab button presses in journal and runs onClick with button clicked as parameter.
		UIEventListener.Get (viewTab1).onClick += this.onClick;
		UIEventListener.Get (viewTab2).onClick += this.onClick;
		UIEventListener.Get (viewTab3).onClick += this.onClick;

		//Listens for button presses in poiView and runs onClick with button clicked as parameter.
		UIEventListener.Get(poiButton1).onClick += this.onClick;
		UIEventListener.Get(poiButton2).onClick += this.onClick;
		UIEventListener.Get(poiButton3).onClick += this.onClick;

		viewTabList = new List<GameObject>();
		viewTabList.Add(viewTab1);
		viewTabList.Add(viewTab2);
		viewTabList.Add(viewTab3);

		//List of person of interest portrait buttons.
		poiButtonList = new List<GameObject>();
		poiButtonList.Add (poiButton1);
		poiButtonList.Add (poiButton2);
		poiButtonList.Add (poiButton3);

		initPoIView ();
	}

	//Single onclick function for any button in the journal.
	void onClick(GameObject button){
		if(viewTabList != null && viewTabList.Contains(button)){
			Debug.Log ("won't happen yet");
		}
		else if(poiButtonList.Contains(button)){
			changePOI(poiButtonList.IndexOf(button));
			Debug.Log ("poiButton!");
		}
		else {
			Debug.Log ("objectbutton!");
		}
	}

	//----- Button type functions
	//Changes view when view tab is clicked.
	void changeView(int viewNumber){
	}

	//Changes PoI when a PoI portrait is clicked.
	void changePOI(int poiNumber){
		if(GameManager.npcList[poiNumber].isVisible()){
			nameLabel.text = personsOfInterest[poiNumber].getElementName();
			descriptionLabel.text = personsOfInterest[poiNumber].getDescription();
		}
		else {
			nameLabel.text = emptyName; 
			descriptionLabel.text = emptyName;
		}
	}

	//Changes object/weapon being viewed when portrait is clicked.
	void changeObject(int objectNumber){
	}

	//Initialize PoI view.
	public void initPoIView(){
		for (int i = 0; i < personsOfInterest.Count; i++) {
			Debug.Log (i);
			if(personsOfInterest[i] != null){
				//Debug.Log("getting image " +i+ ": " + personsOfInterest[i].getProfileImage ().ToString ());
				//GetComponent is slow, will use gameobject find component in children in later iteration.
				poiButtonList[i].gameObject.GetComponent<UI2DSprite>().sprite2D = personsOfInterest[i].getProfileImage();
			}
			else {
				Debug.LogError("Im null");
				poiButtonList[i].gameObject.GetComponent<UI2DSprite>().sprite2D = emptyPortrait;
			}
		}
	}

	public void changePoIView(NPC n){
		//if (indexPoI <= MAX_NPC - 1) {
		//	personsOfInterest[i]=n;
			Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		//	Debug.Log("getting image " +i+ ": " + personsOfInterest[i].getProfileImage ().ToString ());
		//	poiButtonList[i].gameObject.GetComponent<UI2DSprite>().sprite2D = personsOfInterest[i].getProfileImage();
		//}

	}
}
