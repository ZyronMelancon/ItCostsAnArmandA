using UnityEngine;
using System.Collections;

public class ShipSprites : MonoBehaviour {

    public Sprite m_BoostSprite1;
    public Sprite m_BoostSprite2;
    public Transform m_Turbine1;
    public Transform m_Turbine2;

    public GameObject m_Boost;
    public GameObject m_Shot;
    public Transform m_ShootArea;

    private bool spritevisible = false;

    ShipControl ship;

    // Use this for initialization
    void Start () {
        ship = GameObject.Find("Ship").GetComponent<ShipControl>();
    }
	
	// Update is called once per frame
	void Update () {
       StartCoroutine(BoostSprite());
    }

    public IEnumerator BoostSprite()
    {
        if (Input.GetAxisRaw("Boost") == 1 && spritevisible == false && !ship.gameover)
        {
            spritevisible = true;
            GameObject boost1 = Instantiate(m_Boost, m_Turbine1.position, m_Turbine1.rotation) as GameObject;
            GameObject boost2 = Instantiate(m_Boost, m_Turbine2.position, m_Turbine1.rotation) as GameObject;
            boost1.GetComponent<SpriteRenderer>().sprite = m_BoostSprite1;
            boost1.GetComponent<SpriteRenderer>().sprite = m_BoostSprite1;
            yield return new WaitForSeconds(0.03f);
            boost1.GetComponent<SpriteRenderer>().sprite = m_BoostSprite2;
            boost1.GetComponent<SpriteRenderer>().sprite = m_BoostSprite2;
            Destroy(boost1);
            Destroy(boost2);
            spritevisible = false;
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
    }
}
