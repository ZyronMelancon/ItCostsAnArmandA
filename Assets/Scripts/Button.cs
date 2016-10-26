using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Button : MonoBehaviour {

    public void ChangeScene(string scene)
    {
        Application.LoadLevel(scene);
    }
}
