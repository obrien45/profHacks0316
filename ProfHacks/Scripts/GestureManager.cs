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

    bool gestureMatches(Frame otherFrame)
    {
        float totalCloseness = 0;
        List<Hand> otherHands = otherFrame.Hands;
        List<Hand> currentHands = currentFrame.Hands;

        foreach (Hand otherHand in otherHands)
        {
            Vector otherHandDir = otherHand.Direction;
            foreach (Hand currentHand in currentHands)
            {
                Vector currentHandDir = currentHand.Direction;
                for (int i = 0; i < otherHand.Fingers.Count; i++)
                {
                    Finger otherFinger = otherHand.Fingers[i];
                    Finger currentFinger = currentHand.Fingers[i];
                    Vector otherOffset = otherFinger.Direction - otherHandDir;
                    Vector currentOffset = currentFinger.Direction - currentHandDir;
                    totalCloseness += currentOffset.Dot(otherOffset);
                }
            }
        }
        if (totalCloseness < 10)
            return true;
        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoneCalibrating)
        {
            currentFrame = controller.Frame();
            bool foundMatch = false;
            foreach (KeyValuePair<string, Frame> entry in gestures)
            {
                print(entry.Key);
                print(entry.Value.ToString());
                // do something with entry.Value or entry.Key
                if (gestureMatches(entry.Value))
                {
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
