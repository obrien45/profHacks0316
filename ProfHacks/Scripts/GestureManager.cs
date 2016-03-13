using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class GestureManager : MonoBehaviour
{

    Controller controller;
    Frame currentFrame;
    public string currentGesture;
    bool isDoneCalibrating;
    public Dictionary<string, Frame> gestures;
    //maps name of each gesture (english word) to the gesture (ASL sign)

    // Use this for initialization
    void Start()
    {
        controller = new Controller();
        isDoneCalibrating = false;
        gestures = new Dictionary<string, Frame>();
    }

    public void activate()
    {
        isDoneCalibrating = true;
    }

    //add a gesture to the saved gestures, with the given name
    public void addGesture(string name, Frame frame)
    {
        gestures.Add(name, frame);
    }

    public string getCurrentGesture()
    {
        return currentGesture;
    }

    float gestureMatch(Frame otherFrame)
    {
        float angleThreshold = 0.4f;
        float closeness = 200.0f;
        List<Hand> otherHands = otherFrame.Hands;
        List<Hand> currentHands = currentFrame.Hands;
        bool isMatch = false;
        foreach (Hand otherHand in otherHands)
        {
            //return a float that is closeness only if match passes
            Vector otherHandDir = otherHand.Direction;
            foreach (Hand currentHand in currentHands)
            {
                bool couldBeMatch = true;
                Vector currentHandDir = currentHand.Direction;
                for (int i = 0; i < otherHand.Fingers.Count; i++)
                {
                    Finger otherFinger = otherHand.Fingers[i];
                    Finger currentFinger = currentHand.Fingers[i];
                    float otherAngle = otherFinger.Direction.AngleTo(otherHandDir);
                    float currentAngle = currentFinger.Direction.AngleTo(currentHandDir);
                    if (Mathf.Abs(otherAngle - currentAngle) > angleThreshold)
                    {
                        couldBeMatch = false;
                    }
                }
                if(couldBeMatch)
                {
                    isMatch = true;
                    closeness = Mathf.Abs(otherHandDir.AngleTo(currentHandDir));
                }else{
                    closeness = -Mathf.Abs(otherHandDir.AngleTo(currentHandDir));
                }
            }
        }
        return closeness;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoneCalibrating)
        {
            currentFrame = controller.Frame();
            bool foundMatch = false;
            float minDistance = 200.0f;
            foreach (KeyValuePair<string, Frame> entry in gestures)
            {
                //print(entry.Key);
                //print(entry.Value.ToString());
                // do something with entry.Value or entry.Key
                float distance = gestureMatch(entry.Value);
                if (distance >= 0 && distance < minDistance)
                {
                    minDistance = distance;
                    foundMatch = true;
                    currentGesture = entry.Key;
                }
            }
            if(!foundMatch)
            {
                currentGesture = "no match";
            }
        }
    }
}
