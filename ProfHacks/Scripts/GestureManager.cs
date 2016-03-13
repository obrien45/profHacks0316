using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class GestureManager : MonoBehaviour {

    Dictionary<string, Frame> gestures;
    //maps name of each gesture (english word) to the gesture (ASL sign)

	// Use this for initialization
	void Start () {
        gestures = new Dictionary<string, Frame>();
	}

    public void activate()
    {

    }

    //add a gesture to the saved gestures, with the given name
    public void addGesture(string name, Frame frame)
    {
        gestures.Add(name, frame);
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
