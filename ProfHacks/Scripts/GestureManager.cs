﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class GestureManager : MonoBehaviour
{

    Controller controller;
    Frame currentFrame;
    public string currentGesture;
    bool isDoneCalibrating;
    Dictionary<string, Frame> gestures;
    //maps name of each gesture (english word) to the gesture (ASL sign)

    // Use this for initialization
    void Start()
    {
        Controller controller = new Controller();
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

    bool gestureMatches(Frame otherFrame)
    {
        float totalCloseness = 0;
        List<Hand> otherHands = otherFrame.Hands;
        List<Hand> currentHands = currentFrame.Hands;

        foreach(Hand otherHand in otherHands)
        {
            List<Vector> otherFingerOffsets = new List<Vector>();
            Vector otherHandCenter = otherHand.PalmPosition;
            foreach(Finger otherFinger in otherHand.Fingers)
            {
                Vector offset = otherFinger.Direction - otherHandCenter;
                otherFingerOffsets.Add(offset);
            }
            foreach(Hand currentHand in currentHands)
            {
               // List<Vector> currentFingerOffsets = new List<Vector>();
                Vector currentHandCenter = currentHand.PalmPosition;
                foreach (Finger currentFinger in currentHand.Fingers)
                {
                    foreach (Vector difference in otherFingerOffsets)
                    {
                        Vector currentOffset = currentFinger.Direction - currentHandCenter;
                        totalCloseness += currentOffset.Dot(difference);
                    }
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
            foreach (KeyValuePair<string, Frame> entry in gestures)
            {
                // do something with entry.Value or entry.Key
                if (gestureMatches(entry.Value))
                    currentGesture = entry.Key;  
            }
        }
    }
}
