using UnityEngine;
using System.Collections;

public class ScrollUV2 : MonoBehaviour
{

    public float m_ScrollSpeed = 1.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.y += Time.deltaTime * m_ScrollSpeed;

        mat.mainTextureOffset = offset;
    }
}
