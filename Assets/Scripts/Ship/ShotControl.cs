using UnityEngine;
using System.Collections;

public class ShotControl : MonoBehaviour {
    
    public float m_ShotSpeed = 10.0f;
    public float m_ShotDamage = 10.0f;
    public float m_TimeUntilDestroy = 1.0f;

    public ParticleSystem m_ShotFX;


    private float m_ShotAngle = 5f;

    private float hinput;

    Rigidbody2D m_shotPos;

	// Use this for initialization
	void Start () {
        m_shotPos = GetComponent<Rigidbody2D>();
        hinput = Input.GetAxisRaw("Horizontal");
        Destroy(gameObject, 0.7f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_shotPos.velocity = new Vector2(hinput * m_ShotAngle, m_ShotSpeed);
	}

    void OnDestroy()
    {
        m_ShotFX.transform.parent = null;
        m_ShotFX.Play();
        Destroy(m_ShotFX.gameObject, m_ShotFX.duration);
    }
}
