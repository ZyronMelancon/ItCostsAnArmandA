using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipControl : MonoBehaviour {

    public float m_BaseSpeed = 1.0f;
    public float m_BoostMultiplier = 1.5f;
    public float m_SlowMultiplier = 0.5f;
    public float m_TurnTilt = 10f;
    public float m_TiltSpeed = 1f;
    public GameObject m_ShotSmall;
    public Transform m_ShootPos;
    public Slider m_ArmJoint;
    public Sprite m_WingsIn;
    public Sprite m_WingsOut;
    public GameObject m_Grabber;

    public int m_ShotsFired = 0;
    public int m_Fuel = 100;
    public int m_Health = 100;
    public bool gameover = false;

    Rigidbody2D m_Body;
    Transform m_ShipPos;
    public ParticleSystem m_Explosion;
    public AudioSource m_ExplosionSFX;

    private bool shooting = false;
    private bool boosting = false;
    private bool timething = false;

    private float roboarmlength = 0;
    private bool callOnce = false;

	// Use this for initialization
	void Start () {
        m_Body = GetComponent<Rigidbody2D>();
        m_ShipPos = GetComponent<Transform>();
        StartCoroutine(fuelLoss());
        StartCoroutine(roboarm());
    }
	
	// Update is called once per frame
	void Update ()
    {
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        StartCoroutine(Shoot());

        grabber();

        if (Input.GetAxisRaw("Fire2") == 0)
            shipRotate(Input.GetAxisRaw("Horizontal"));

        

        if (m_Health <= 0)
        {
            m_Health = 0;
            gameover = true;
            Destroy(gameObject);
        }

        if(m_Fuel <= 0)
        {
            m_Fuel = 0;
            gameover = true;
        }
    }

    IEnumerator Shoot()
    {
        if (Input.GetAxisRaw("Fire1") == 1 && shooting == false)
        {
            shooting = true;
            Rigidbody2D shotInstance = Instantiate(m_ShotSmall, m_ShootPos.position, m_ShootPos.rotation) as Rigidbody2D;
            m_ShotsFired++;
            yield return new WaitForSeconds(0.1f);
            shooting = false;
        }
    }

    IEnumerator fuelLoss()
    {
        while (!gameover)
        {
            if (m_Fuel >= 0 && Input.GetAxisRaw("Boost") == 1 && !boosting)
            {

                boosting = true;
                yield return new WaitForSeconds(1f);
                m_Fuel--;
                boosting = false;

            }
            else if (m_Fuel >= 0 && !boosting && !timething)
            {
                timething = true;
                yield return new WaitForSeconds(2.5f);
                m_Fuel--;
                timething = false;
            }
        }
    }

    IEnumerator roboarm()
    {
            if (Input.GetAxisRaw("Fire2") == 1 && !callOnce)
            {
                callOnce = true;
                GetComponent<SpriteRenderer>().sprite = m_WingsIn;
                while (roboarmlength < 30)
                {
                    yield return new WaitForSeconds(0.02f);
                    roboarmlength += 1;
                    m_ArmJoint.value = roboarmlength;
                }
                while (roboarmlength > 0)
                {
                    yield return new WaitForSeconds(0.02f);
                    roboarmlength -= 1;
                    m_ArmJoint.value = roboarmlength;
                }
                GetComponent<SpriteRenderer>().sprite = m_WingsOut;
            }
            callOnce = false;
    }

    void grabber()
    {
        if(Input.GetAxisRaw("Fire2") == 1)
        {
            GetComponent<SpriteRenderer>().sprite = m_WingsIn;
            m_Grabber.SetActive(true);
            if (m_Grabber.transform.localPosition.y < 1)
                roboarmlength = 1;
            else
                roboarmlength = 0;
            m_Grabber.transform.Translate(0,roboarmlength/5,0);
        }
        else
        {
            if (m_Grabber.transform.localPosition.y > 0)
                roboarmlength = 1;
            else
                roboarmlength = 0;
            m_Grabber.transform.Translate(0, -roboarmlength/5, 0);
            if (roboarmlength == 0)
                m_Grabber.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = m_WingsOut;
        }
    }

    public void Move(float hinput, float vinput)
    {
        Vector3 moveVel = m_Body.velocity;
        float currentspeed = 0;

        if(roboarmlength > 0)
        {
            hinput = 0;
        }

        if (!gameover)
        {
            if (Input.GetAxisRaw("Boost") == 1)
            {
                currentspeed = m_BaseSpeed * m_BoostMultiplier;
                moveVel.x = hinput * currentspeed;
                moveVel.y = vinput * currentspeed;
            }
            else if (Input.GetAxisRaw("Slow") == 1)
            {
                currentspeed = m_BaseSpeed * m_SlowMultiplier;
                moveVel.x = hinput * currentspeed;
                moveVel.y = vinput * currentspeed;
            }
            else
            {
                currentspeed = m_BaseSpeed;
                moveVel.x = hinput * currentspeed;
                moveVel.y = vinput * currentspeed;
            }
        }

        if(vinput == 0 && Input.GetAxisRaw("Boost") == 0)
        {
            moveVel.y = 0;
            moveVel.y = moveVel.y + (-5.8f - m_ShipPos.position.y);
        }
        else if(gameover)
        {
            moveVel.y = 0;
            moveVel.x = 0;
            moveVel.y = moveVel.y + (-5.8f - m_ShipPos.position.y);
        }

        if (m_ShipPos.position.y >= 5.8f)
            moveVel.y = moveVel.y - currentspeed;
        if (m_ShipPos.position.y <= -5.8f)
            moveVel.y = moveVel.y + currentspeed;
        if (m_ShipPos.position.x >= 3f)
            moveVel.x = moveVel.x - currentspeed;
        if (m_ShipPos.position.x <= -3f)
            moveVel.x = moveVel.x + currentspeed;

        m_Body.velocity = moveVel;

    }

    public void shipRotate(float hinput)
    {
        float rotationSum = hinput * -m_TurnTilt;
        m_Body.MoveRotation(rotationSum);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyShot")
        {
            EnemyShotControl enshot = other.gameObject.GetComponent<EnemyShotControl>();
            m_Health -= enshot.m_ShotDamage;
            Destroy(other.gameObject);
        }
    }

    void OnDestroy()
    {
        m_Explosion.transform.parent = null;
        m_Explosion.Play();
        m_ExplosionSFX.Play();
        Destroy(m_Explosion.gameObject, m_Explosion.duration + 3);
    }

}
