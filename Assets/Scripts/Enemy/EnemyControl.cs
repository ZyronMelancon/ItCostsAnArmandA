using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

    public GameObject m_Shot;
    public ParticleSystem m_Explosion;
    public float m_ShootSpeed;
    public float m_StartPos;
    public float m_ZoomInPos;
    public float m_MoveSpeed;
    public float m_Health;

    AudioSource m_ExplSFX;
    HUDUpdate HUD;
    EnemyManager EnMan;
    ShipControl ship;
    Rigidbody2D m_Body;

    void Start ()
    {
        m_Body = GetComponent<Rigidbody2D>();
        ship = GameObject.Find("Ship").GetComponent<ShipControl>();
        HUD = GameObject.Find("HUD").GetComponent<Canvas>().GetComponent<HUDUpdate>();
        StartCoroutine(Shoot());
        EnMan = GameObject.Find("HUD").GetComponent<EnemyManager>();
        m_ExplSFX = GameObject.Find("EnemyKills").GetComponent<AudioSource>();
	}
	
    IEnumerator Shoot()
    {
        while(!ship.gameover)
        {
            yield return new WaitForSeconds(m_ShootSpeed);
            if (!ship.gameover)
            {
                Rigidbody2D shotInstance = Instantiate(m_Shot, transform.position, transform.rotation) as Rigidbody2D;
            }
        }
    }


	void Update ()
    {
	    if(m_Health <= 0)
        {
            HUD.enemykills++;
            EnMan.m_EnemiesAlive--;
            Destroy(gameObject);
        }
        Move();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerShot")
        {
            Destroy(other.gameObject);
            m_Health -= 5;
            HUD.hits++;
        }
    }

    void OnDestroy()
    {
        m_Explosion.transform.parent = null;
        m_Explosion.Play();
        m_ExplSFX.Play();
        Destroy(m_Explosion.gameObject, m_Explosion.duration + 1f);
    }

    void Move()
    {
        Vector3 moveVel = m_Body.velocity;

        moveVel.y = 0;

        if(!ship.gameover)
            moveVel.y = moveVel.y + (m_ZoomInPos - transform.position.y);
        else
            moveVel.y = moveVel.y + (11 - transform.position.y);

        m_Body.velocity = moveVel;
    }
}
