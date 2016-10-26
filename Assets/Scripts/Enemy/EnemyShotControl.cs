using UnityEngine;
using System.Collections;

public class EnemyShotControl : MonoBehaviour {

    public float m_ShotSpeedMultiplier = 1f;
    public int m_ShotDamage;
    public float m_TimeUntilDestroy = 1.0f;

    public ParticleSystem m_ShotFX;

    
    private float m_ShotAngle = 5f;

    private float hinput;

    Rigidbody2D m_shotPos;
    Transform shipPos;
    Vector2 target;
    ShipControl ship;

    // Use this for initialization
    void Start()
    {
        shipPos = GameObject.Find("Ship").GetComponent<Transform>();
        m_shotPos = GetComponent<Rigidbody2D>();
        hinput = Input.GetAxisRaw("Horizontal");

        ship = GameObject.Find("Ship").GetComponent<ShipControl>();
        target = shipPos.position - transform.position;

        if (ship.gameover)
        {
            Destroy(gameObject);
        }

        Destroy(gameObject, m_TimeUntilDestroy);

    }

    // Update is called once per frame
    void Update()
    {
        m_shotPos.velocity = target * m_ShotSpeedMultiplier;

        if(ship.gameover)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        m_ShotFX.transform.parent = null;
        m_ShotFX.Play();
        Destroy(m_ShotFX.gameObject, m_ShotFX.duration);
    }
}
