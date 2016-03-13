using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GestureDisplay2 : MonoBehaviour
{
    public GestureConcat gestConcat;
    public GameObject gestureManager;

    // Use this for initialization
    void Start()
    {
        gestConcat = GetComponent<GestureConcat>();
    }

    // Update is called once per frame
    void Update()
    {
        string result = gestConcat.result;
        this.GetComponent<Text>().text = result; 
    }
}

