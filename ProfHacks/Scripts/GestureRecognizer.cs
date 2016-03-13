using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class GestureRecognizer : MonoBehaviour {
    GestureManager manager;
    public GestureManager secondaryManager;
    Controller controller;

    public List<string> gestureNames;
    public List<string> secondaryGestureNames;

	// Use this for initialization
	void Start () {
        controller = new Controller();
        manager = GetComponent<GestureManager>();
	}

    public string getNextGestureName()
    {
        if (gestureNames.Count > 0)
            return gestureNames[0];
        else if (secondaryGestureNames.Count > 0)
            return secondaryGestureNames[0];
        else
            return "none more";
    }
	
	// Update is called once per frame
	void Update () {
        if (gestureNames.Count > 0 || secondaryGestureNames.Count > 0)
        {
            if (Input.GetKeyDown("f"))
            {
                if (gestureNames.Count <= 0 && secondaryManager != null)
                {
                    secondaryManager.addGesture(secondaryGestureNames[0], controller.Frame());
                    secondaryGestureNames.RemoveAt(0);
                }
                else
                {
                    manager.addGesture(gestureNames[0], controller.Frame());
                    gestureNames.RemoveAt(0);
                }
                //add this frame to the saved gesture list
                if(gestureNames.Count == 0 && secondaryGestureNames.Count == 0)
                {
                    manager.activate();
                    if(secondaryManager != null)
                        secondaryManager.activate();
                    Destroy(this);
                }
            }
        }
        else
        {
            manager.activate();
            if (secondaryManager != null)
                secondaryManager.activate();
            Destroy(this);
        }
	}
}
