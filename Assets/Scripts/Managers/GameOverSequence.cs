using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverSequence : MonoBehaviour {

    public Text m_Score;
    public Text m_Time;
    public Text m_Shots;
    public Text m_Hits;
    public Text m_Kills;
    public Text m_Wave;
    public Text m_Options;

    ShipControl ship;
    HUDUpdate stats;
    int score;
    int time;
    int newscore;
    int timeFraction;

	// Use this for initialization
	void Start () {
        stats = GameObject.Find("HUD").GetComponent<HUDUpdate>();

        score = stats.score;
        time = stats.time;

        newscore = stats.score;

        m_Score.text = "Score\n" + score;
        m_Time.text = "Time Bonus\n" + time;
        m_Shots.text = "Shots Fired\n" + stats.shots;
        m_Hits.text = "Hits\n" + stats.hits;
        m_Kills.text = "Enemy Kills\n" + stats.enemykills;
        m_Wave.text = "Wave\n" + stats.wave;
        m_Options.text = " ";

        StartCoroutine(scoreMultiply());
    }
	
	// Update is called once per frame
	void Update () {
        
        m_Score.text = "Score\n" + newscore;
        m_Time.text = "Time Bonus\n" + time;
        m_Shots.text = "Shots Fired\n" + stats.shots;
        m_Hits.text = "Hits\n" + stats.hits;
        m_Kills.text = "Enemy Kills\n" + stats.enemykills;


        if (time == 0 && Input.GetAxisRaw("Fire1") == 1)
        {
            Application.LoadLevel("SurvivalGame");
        }
        else if (time == 0 && Input.GetAxisRaw("Fire2") == 1)
        {
            Application.Quit();
        }
    }

    IEnumerator scoreMultiply()
    {
        yield return new WaitForSeconds(2);
        for (; time > 0; time--)
        {
            yield return new WaitForSeconds(0.02f);
            newscore += 500;
        }
        m_Options.text = "Press 'Z' to replay\nPress 'X' to exit";
    }

}
