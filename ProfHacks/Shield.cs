using UnityEngine;
using System.Collections;
using Leap;

public class Shield : MonoBehaviour {

    public GameObject gestureManager;

    // Use this for initialization
    void Start () {
        this.GetComponent<MeshRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        MeshRenderer mesh = this.GetComponent<MeshRenderer>();
        if (gestureManager.GetComponent<GestureManager>().getCurrentGesture().Equals("Shield"))
        {
            if(!mesh.isVisible)
            {
                mesh.enabled = true;
            }
        }
        else if(mesh.isVisible)
        {
            mesh.enabled = false;
        }
    }
}
