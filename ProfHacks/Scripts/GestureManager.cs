using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class GestureManager : MonoBehaviour
{
    GestureConcat concatonator;
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
        concatonator = new GestureConcat();
        concatonator.manager = this;
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

    public float matchHands(Hand savedHand, Hand currentHand)
    {
        float angleThreshold = 0.5f;
        float closeness = -1.0f;
        Vector otherHandDir = savedHand.Direction;
        Vector otherHandPos = savedHand.PalmPosition;
        bool couldBeMatch = true;
        Vector currentHandDir = currentHand.Direction;
        Vector currentHandPos = currentHand.PalmPosition;
        for (int i = 0; i < savedHand.Fingers.Count; i++)
        {
            Finger otherFinger = savedHand.Fingers[i];
            Finger currentFinger = currentHand.Fingers[i];
            float otherAngle = otherFinger.Direction.AngleTo(otherHandDir);
            float currentAngle = currentFinger.Direction.AngleTo(currentHandDir);
            Vector otherFingerPos = otherFinger.TipPosition;
            Vector currentFingerPos = currentFinger.TipPosition;
            float currentDist = currentFingerPos.DistanceTo(currentHandPos);
            float otherDist = otherFingerPos.DistanceTo(otherHandPos);
            if (currentDist > 1.3 * otherDist)
            {
                couldBeMatch = false;
            }
            if (Mathf.Abs(otherAngle - currentAngle) > angleThreshold)
            {
                couldBeMatch = false;
            }
        }
        if (couldBeMatch)
        {
            closeness = Mathf.Abs(otherHandDir.AngleTo(currentHandDir));
        }
        else
        {
            closeness = -1.0f;
        }
        return closeness;
    }

    float gestureMatch(Frame otherFrame)
    {
        List<Hand> otherHands = otherFrame.Hands;
        List<Hand> currentHands = currentFrame.Hands;
        if (otherHands.Count == 1)
        {
            foreach(Hand currentHand in currentHands)
            {
                float match = matchHands(otherHands[0], currentHand);
                if(match != -1.0f)
                    return match;

            }
            return -1.0f;
            /*
            return matchHands(otherHands[0], currentFrame)
            foreach (Hand otherHand in otherHands)
            {
                //return a float that is closeness only if match passes
                Vector otherHandDir = otherHand.Direction;
                Vector otherHandPos = otherHand.PalmPosition;
                foreach (Hand currentHand in currentHands)
                {
                    bool couldBeMatch = true;
                    Vector currentHandDir = currentHand.Direction;
                    Vector currentHandPos = currentHand.PalmPosition;
                    for (int i = 0; i < otherHand.Fingers.Count; i++)
                    {
                        Finger otherFinger = otherHand.Fingers[i];
                        Finger currentFinger = currentHand.Fingers[i];
                        float otherAngle = otherFinger.Direction.AngleTo(otherHandDir);
                        float currentAngle = currentFinger.Direction.AngleTo(currentHandDir);
                        Vector otherFingerPos = otherFinger.TipPosition;
                        Vector currentFingerPos = currentFinger.TipPosition;
                        float currentDist = currentFingerPos.DistanceTo(currentHandPos);
                        float otherDist = otherHandPos.DistanceTo(otherHandPos);
                        if (currentDist > 1.3 * otherDist)
                        {
                            couldBeMatch = false;
                        }
                        if (Mathf.Abs(otherAngle - currentAngle) > angleThreshold)
                        {
                            couldBeMatch = false;
                        }
                    }
                    if (couldBeMatch)
                    {
                        isMatch = true;
                        closeness = Mathf.Abs(otherHandDir.AngleTo(currentHandDir));
                    }
                    else
                    {
                        closeness = -Mathf.Abs(otherHandDir.AngleTo(currentHandDir));
                    }
                }
            }
            return closeness;*/
        }
        else if (otherHands.Count == 2)
        {
            if (currentHands.Count == 2)
            {

                Hand currentHandOne = currentHands[0];
                Hand currentHandTwo = currentHands[1];
                Hand savedHandOne = otherHands[0];
                Hand savedHandTwo = otherHands[1];
                float firstHandMatch = matchHands(currentHandOne, savedHandOne);
                if(firstHandMatch != -1)
                {
                    float twoTwoHandMatch = matchHands(currentHandTwo, savedHandTwo);
                    if(twoTwoHandMatch != -1)
                    {
                        return (twoTwoHandMatch + firstHandMatch)/2.0f;
                    }
                }else{
                    float secondHandMatch = matchHands(currentHandTwo, savedHandOne);
                    if(secondHandMatch != -1)
                    {
                        float twoTwoHandMatch = matchHands(currentHandOne, savedHandTwo);
                        if(twoTwoHandMatch != -1)
                        {
                            return (twoTwoHandMatch + secondHandMatch )/2.0f;
                        }
                    }
                }
            }
            return -1;
        }
        else
            return -1;
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
