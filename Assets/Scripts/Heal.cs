using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour {

    ShipControl ship;
    public AudioSource m_HealSound;

	// Use this for initialization
	void Start () {
        ship = GameObject.Find("Ship").GetComponent<ShipControl>();
	}

	void Update () {
	
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Heal")
        {
            ship.m_Health = 100;
            ship.m_Fuel = 50;
            Destroy(other.gameObject);
            m_HealSound.Play();
        }
    }

}
