using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public AudioClip m_StartMusic;
	public AudioClip m_GameMusic;
	public AudioClip m_EndMusic;

	static MusicPlayer instance = null;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			
			// Initialize music
			m_Music = GetComponent<AudioSource>();
			m_Music.clip = m_StartMusic;
			m_Music.loop = true;
			m_Music.Play();
		}
		
	}
	
	void OnLevelWasLoaded(int level) {
		//print ("Level: " + level);
		m_Music.Stop();
		
		if(level == 0)
			m_Music.clip = m_StartMusic;
			
		if(level == 1)
			m_Music.clip = m_GameMusic;
		
		if(level == 2)
			m_Music.clip = m_EndMusic;
			
		m_Music.loop = true;
		m_Music.Play();
	}
	
	private AudioSource m_Music;
}
