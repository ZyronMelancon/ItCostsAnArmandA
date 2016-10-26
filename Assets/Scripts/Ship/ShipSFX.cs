using UnityEngine;
using System.Collections;

public class ShipSFX : MonoBehaviour {

    public AudioSource m_FireSound;
    public AudioSource m_BoostSound;
    public AudioSource m_HealSound;
    public AudioSource m_DieSound;

    private bool shooting = false;
    private bool boosting = false;
    private bool healing = false;
    private bool dying = false;

    ShipControl ship;

	// Use this for initialization
	void Start () {
        ship = GameObject.Find("Ship").GetComponent<ShipControl>();
	}
	
	// Update is called once per frame
	void Update () {
        Boost();
	}

    public void Boost()
    {
        if(Input.GetAxisRaw("Boost") == 1 && boosting == false && !ship.gameover)
        {
            boosting = true;
            m_BoostSound.Play();
        }
        else if (Input.GetAxisRaw("Boost") == 0)
        {
            boosting = false;
            if(m_BoostSound.isPlaying)
            {
                m_BoostSound.Stop();
            }
        }

    }
}
