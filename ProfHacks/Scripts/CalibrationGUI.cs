using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CalibrationGUI : MonoBehaviour {

    public GameObject gestureManager;
    public GameObject gestureNameText;
    public GameObject gestureDisplayCanvas;

	// Use this for initialization
	void Start () {
        gestureDisplayCanvas.GetComponent<Canvas>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        GestureRecognizer recognizer = gestureManager.GetComponent<GestureRecognizer>();
        if (recognizer != null)
        {
            gestureNameText.GetComponent<Text>().text = recognizer.getNextGestureName();
        }
        else
        {
            this.GetComponent<Canvas>().enabled = false;
            gestureDisplayCanvas.GetComponent<Canvas>().enabled = true;
        }
	}
}
