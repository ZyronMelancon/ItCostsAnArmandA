using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public GameObject m_SmallEnemy;
    public GameObject m_BigEnemy;
    public int m_Wave = 0;

    EnemyControl m_SmallControl;
    EnemyControl m_BigControl;

    private float m_EnemySpeed = 10f;
    private float m_EnemyFireSpeed = 0.2f;
    [HideInInspector] public int m_EnemiesAlive = 0;

    float shootspeed = 1.0f;

    ShipControl ship;

    bool waitTime = false;

	// Use this for initialization
	void Start ()
    {
        ship = GameObject.Find("Ship").GetComponent<ShipControl>();
	}

	void Update ()
    {
        if (!ship.gameover)
        {
            StartCoroutine(Wave(m_Wave));
        }
	}

    IEnumerator Wave(int wavnum)
    {
        if (m_EnemiesAlive == 0 || waitTime == true)
        {
            if (m_EnemiesAlive == 0 && wavnum % 10 == 0 && wavnum > 1)
            {
                m_Wave++;
                m_EnemiesAlive++;
                SpawnBig();
                waitTime = false;
                StartCoroutine(timeWait());
            }

            if (m_EnemiesAlive == 0 && wavnum % 10 > 0)
            {
                m_Wave++;
                if (shootspeed > 0.1f)
                {
                    shootspeed -= 0.025f;
                }
                waitTime = false;
                for (int i = wavnum % 10; i > 0; i--)
                {
                    Spawn();
                    m_EnemiesAlive++;
                    yield return new WaitForSeconds(0.2f);
                }
                StartCoroutine(timeWait());
            }
        }


    }

    void Spawn()
    {
        Vector3 randomX = new Vector3(Random.Range(-3.0f, 3.0f), 9,0);
        GameObject smallInst = Instantiate(m_SmallEnemy,randomX,Quaternion.identity) as GameObject;
        EnemyControl smallControl = smallInst.GetComponent<EnemyControl>();
        smallControl.m_ShootSpeed = shootspeed + Random.Range(0f, 0.2f);
        smallControl.m_ZoomInPos = Random.Range(2.4f, 5.5f);
    }

    void SpawnBig()
    {
        GameObject bigInst = Instantiate(m_BigEnemy, new Vector3(0,9,0), Quaternion.identity) as GameObject;
        EnemyControl bigControl = bigInst.GetComponent<EnemyControl>();
        bigControl.m_ShootSpeed = shootspeed;
    }

    IEnumerator timeWait()
    {
        int wavy = m_Wave;

        waitTime = false;

        yield return new WaitForSeconds(10);
        if (wavy == m_Wave)
        {
            waitTime = true;
        }
    }
}
