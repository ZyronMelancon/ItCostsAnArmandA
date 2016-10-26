using UnityEngine;
using System.Collections;

public class MusicSwitch : MonoBehaviour {

    public AudioSource m_TutorialMusic;
    public AudioSource m_MainMusic;

    private bool playing = false;

	// Update is called once per frame
	void Update () {
	    if(!m_TutorialMusic.isPlaying && !playing)
        {
            m_MainMusic.Play();
            playing = true;
        }
	}
}
