using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    
    public int m_Wave = 1;
    public EnemyManager m_EnemyManager;
    public AudioSource m_Music;
    public CanvasGroup m_GameOverScreen;
    public GameObject m_GameOverObj;

    public ShipControl ship;

    private int EnemiesAlive = 0;

    bool callonce = false;

    // Use this for initialization
    void Start ()
    {
        ship = GameObject.Find("Ship").GetComponent<ShipControl>();
        

	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(ship.gameover && ship.m_Fuel > 0)
        {
            m_Music.Stop();
        }
        else if (ship.gameover && ship.m_Fuel <= 0)
        {
            if (m_Music.pitch > 0)
            {
                m_Music.pitch -= 0.005f;
            }
            else if (m_Music.pitch < 0)
            {
                m_Music.pitch = 0;
            }
        }

        if(ship.gameover && callonce == false)
        {
            callonce = true;
            StartCoroutine(endSeq());
        }
	}

    IEnumerator endSeq()
    {
        yield return new WaitForSeconds(4);
        m_GameOverObj.SetActive(true);
    }

}
