using UnityEngine;
using System.Collections;

public class TriggerButton : MonoBehaviour {

    public GameObject target;
    public string functionName;

	// Use this for initialization
	void Start () {
	
	}
	
    void OnTriggerEnter(Collider collider)
    {
        target.SendMessage(functionName);
    }
}
