using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroSequence : MonoBehaviour {

    public Text m_Display;
    public AudioSource m_Music;

	// Use this for initialization
	void Start () {
        StartCoroutine(Display());
	}
	
    IEnumerator Display()
    {
        m_Display.text = "A Game by AuSolaris";
        yield return new WaitForSeconds(4);
        m_Display.text = "All music, sprites,\nsounds, scripts made\nunder 48 hours\n(With later edits)";
        yield return new WaitForSeconds(4);
        m_Display.text = "10/21/16-10/23/16";
    }


	// Update is called once per frame
	void Update () {
	    if (m_Music.isPlaying == false)
        {
            Application.LoadLevel("SurvivalGame");
        }
	}
}
