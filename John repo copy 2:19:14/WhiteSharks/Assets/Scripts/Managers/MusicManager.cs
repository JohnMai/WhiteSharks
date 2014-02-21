/* by John Cotrel
Music Manager for White Shark 2/20/14
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour {
	private static MusicManager instance = null;

	public List<AudioSource> Jams;

	public static MusicManager Instance {
		get {
			if (instance == null) {
				Debug.Log("Instance null, creating new MusicManager");
				instance = new GameObject("MusicManager").AddComponent<MusicManager>();
			}
			return instance;
		}
	}

	void Awake() {
		instance = this;
		DontDestroyOnLoad(gameObject);
	}	

	/*public void Play() {
		audio.Play();
	}

	public void Stop() {
		audio.Stop();
	}*/

	public void playTrack(int tracknum) {
		Jams[tracknum].Play();
	}

	public void stopTrack(int tracknum) {
		Jams[tracknum].Stop();
	}

	public void stopTrack() {
		foreach (AudioSource i in Jams) {
			i.Stop();
		}
	}

	public void Init() {
		Jams = new List<AudioSource>();

		AudioSource FinJam = (AudioSource)gameObject.AddComponent<AudioSource>();
		AudioClip finSong;
		finSong = (AudioClip)Resources.Load("Music/Fin/Ashton Manor");
		FinJam.clip = finSong;
		FinJam.loop = true;

		AudioSource BellyJam = (AudioSource)gameObject.AddComponent<AudioSource>();
		BellyJam.clip = (AudioClip)Resources.Load("Music/Belly/Son of A Rocket");
		BellyJam.loop = true;

		AudioSource AlleyJam = (AudioSource)gameObject.AddComponent<AudioSource>();
		AlleyJam.clip = (AudioClip)Resources.Load("Music/Alley/One Sly Move");
		AlleyJam.loop = true;

		Jams.Add(FinJam);
		Jams.Add(BellyJam);
		Jams.Add(AlleyJam);

		//FinJam.Play();

		//audio.Stop();
	}
	

	public void updateBGM(int previousRoomIndex, int currentRoomIndex) {
		print("GOING FROM "+previousRoomIndex+" TO "+currentRoomIndex);
		if (currentRoomIndex > previousRoomIndex) {
			if (currentRoomIndex == 3) {
				stopTrack();
				playTrack(1);
			}
			else if (currentRoomIndex == 4) {
				stopTrack();
				playTrack(2);
			}
		}
		else {
			if (currentRoomIndex == 3) {
				stopTrack();
				playTrack(1);
			}
			else if (currentRoomIndex == 2) {
				stopTrack();
				playTrack(0);
			}
		}
	}
}