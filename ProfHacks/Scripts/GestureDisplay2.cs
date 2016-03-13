using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GestureDisplay2 : MonoBehaviour
{

    public GameObject gestureManager;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = gestureManager.GetComponent<GestureManager>().getCurrentGesture();
    }
}

