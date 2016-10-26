using UnityEngine;
using System.Collections;

public class ScrollUV : MonoBehaviour {

    public float m_ScrollSpeed = 1.5f;

    ShipControl ship;

	// Use this for initialization
	void Start () {
        ship = GameObject.Find("Ship").GetComponent<ShipControl>();
	}
	
	// Update is called once per frame
	void Update () {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        if (!ship.gameover)
        {
            if (Input.GetAxisRaw("Boost") == 1)
                m_ScrollSpeed = 2.5f;
            else
                m_ScrollSpeed = 1.5f;
        }
        else
        {
            if (m_ScrollSpeed > 0)
                m_ScrollSpeed -= 0.01f;
            else if (m_ScrollSpeed < 0)
                m_ScrollSpeed = 0;
        }

        offset.y += Time.deltaTime * m_ScrollSpeed;

        mat.mainTextureOffset = offset;
	}
}
