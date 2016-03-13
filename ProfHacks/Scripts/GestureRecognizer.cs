using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class GestureRecognizer : MonoBehaviour {
    GestureManager manager;
    Controller controller;

    public List<string> gestureNames;

	// Use this for initialization
	void Start () {
        controller = new Controller();
	}

    public string getNextGestureName()
    {
        return gestureNames[0];
    }
	
	// Update is called once per frame
	void Update () {
        if (gestureNames.Count > 0)
        {
            if (Input.GetKeyDown("F"))
            {
                manager.addGesture(gestureNames[0], controller.Frame());
                //add this frame to the saved gesture list
                gestureNames.RemoveAt(0);
            }
        }
        else
        {
            manager.activate();
            Destroy(this);
        }
	}
}
