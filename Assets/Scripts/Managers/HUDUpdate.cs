using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDUpdate : MonoBehaviour {

    public Text m_Score;
    public Text m_ShotsFired;
    public Text m_Hits;
    public Text m_EnemyKills;
    public Text m_Time;
    public Text m_Health;
    public Text m_Fuel;
    public Text m_Wave;
    public Text m_Tutorial;

    public ShipControl m_ShipStats;
    public EnemyManager m_EnemyManger;

    public int score;
    public int time = 0;
    public int shots = 0;
    public int hits = 0;
    public int enemykills = 0;
    public int wave = 0;
    

	// Use this for initialization
	void Start () {
        StartCoroutine(timer());
        StartCoroutine(tutdisappear());
        m_EnemyManger = GetComponent<EnemyManager>();
    }
	
	// Update is called once per frame
	void Update () {

        shots = m_ShipStats.m_ShotsFired;

        wave = m_EnemyManger.m_Wave;

        score = (hits * 50) + (enemykills * 100);
        m_Score.text = "Score\n" + score;
        m_ShotsFired.text = "Shots Fired\n" + shots;
        m_Hits.text = "Hits\n" + hits;
        m_EnemyKills.text = "Enemy Kills\n" + enemykills;
        m_Time.text = "Time\n" + time;
        m_Health.text = "Health\n" + m_ShipStats.m_Health;
        if (m_ShipStats.m_Health == 0)
            m_Health.color = new Color32(252, 124, 143, 255);
        m_Fuel.text = "Fuel\n" + m_ShipStats.m_Fuel;
        if (m_ShipStats.m_Fuel == 0)
            m_Fuel.color = new Color32(252, 124, 143,255);
        m_Wave.text = "Wave\n" + (m_EnemyManger.m_Wave - 1);

	}

    IEnumerator timer()
    {
        while (!m_ShipStats.gameover)
        {
            yield return new WaitForSeconds(1);
            time++;
        }
    }

    IEnumerator tutdisappear()
    {
        yield return new WaitForSeconds(6);
        m_Tutorial.enabled = false;
    }

}
