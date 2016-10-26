using UnityEngine;
using System.Collections;

public class DestroyShipDeath : MonoBehaviour {

    ShipControl ship;

    void Start()
    {
            ship = GameObject.Find("Ship").GetComponent<ShipControl>();
    }

	void Update ()
    {
	    if (GameObject.Find("Ship") == null || ship.gameover)
        {
            Destroy(gameObject);
        }
	}
}
