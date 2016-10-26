using UnityEngine;
using System.Collections;

public class EnemyControllerBoss : MonoBehaviour {

    public GameObject m_Shot;
    public Transform m_ShootPosL;
    public Transform m_ShootPosR;
    public ParticleSystem m_Explosion;
    public float m_ShootSpeed;
    public float m_StartPos;
    public float m_Health = 100;

    AudioSource m_ExplSFX;
    HUDUpdate HUD;
    EnemyManager EnMan;
    ShipControl ship;
    Rigidbody2D m_Body;

    void Start()
    {
        m_ExplSFX = GameObject.Find("BigExp").GetComponent<AudioSource>();
        m_Body = GetComponent<Rigidbody2D>();
        ship = GameObject.Find("Ship").GetComponent<ShipControl>();
        HUD = GameObject.Find("HUD").GetComponent<Canvas>().GetComponent<HUDUpdate>();
        StartCoroutine(Shoot());
        EnMan = GameObject.Find("HUD").GetComponent<EnemyManager>();
    }

    IEnumerator Shoot()
    {
        while (!ship.gameover)
        {
            yield return new WaitForSeconds(m_ShootSpeed);
            if (!ship.gameover)
            {
                Rigidbody2D shotInstance = Instantiate(m_Shot, m_ShootPosL.position, m_ShootPosL.rotation) as Rigidbody2D;
                yield return new WaitForSeconds(m_ShootSpeed);
                Rigidbody2D shotInstance2 = Instantiate(m_Shot, m_ShootPosR.position, m_ShootPosR.rotation) as Rigidbody2D;
                yield return new WaitForSeconds(m_ShootSpeed);
                Rigidbody2D shotInstance3 = Instantiate(m_Shot, m_ShootPosL.position, m_ShootPosL.rotation) as Rigidbody2D;
                yield return new WaitForSeconds(m_ShootSpeed);
                Rigidbody2D shotInstanc4 = Instantiate(m_Shot, m_ShootPosR.position, m_ShootPosR.rotation) as Rigidbody2D;
                yield return new WaitForSeconds(m_ShootSpeed);
                Rigidbody2D shotInstance5 = Instantiate(m_Shot, m_ShootPosL.position, m_ShootPosL.rotation) as Rigidbody2D;
                yield return new WaitForSeconds(m_ShootSpeed);
                Rigidbody2D shotInstanc6 = Instantiate(m_Shot, m_ShootPosR.position, m_ShootPosR.rotation) as Rigidbody2D;
                yield return new WaitForSeconds(1);
            }
        }
    }


    void Update()
    {
        if (m_Health <= 0)
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

        if (!ship.gameover)
            moveVel.y = moveVel.y + (5f - transform.position.y);
        else
            moveVel.y = moveVel.y + (11 - transform.position.y);

        m_Body.velocity = moveVel;
    }

}
