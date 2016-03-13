using UnityEngine;
using System.Collections;

public class GestureConcat : MonoBehaviour {

    public GestureManager charManager;
    public GestureManager controlManager;
    public string result;
    public string lastCharString;
    public string clear;
    public float timer;
    // Use this for initialization
	void Start () 
    {
        clear = " ";
        result = "";
        lastCharString = charManager.currentGesture;
        timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
        string charString = charManager.getCurrentGesture();
        string commandString = controlManager.getCurrentGesture();
        if (commandString.Equals("clear"))
        {
            result = "";
        }
        else if (!lastCharString.Equals(charString) && !charString.Equals("no match") && commandString.Equals("continue entering") && timer > 1.5f)
        {
            result += charString;
            timer = 0.0f;
        }
	}
}
