using UnityEngine;
using System.Collections;

public class GestureConcat : MonoBehaviour {

    public GestureManager manager;
    public string result;
    public string lastGesture;
    public string clear;
    // Use this for initialization
	void Start () 
    {
        clear = " ";
        result = "";
        lastGesture = manager.currentGesture;
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        string currGesture = manager.currentGesture;
        if (currGesture.Equals(clear))
        {
            result = "";
        }
        if (!lastGesture.Equals(currGesture))
        {
            result += currGesture;
        }
	}
}
