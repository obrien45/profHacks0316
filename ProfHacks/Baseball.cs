using UnityEngine;
using System.Collections;

public class Baseball : MonoBehaviour {
    public GameObject target;
    Rigidbody rigidComponent;
    public float force;
	// Use this for initialization
	void Start () {
        rigidComponent = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        this.rigidComponent.AddForce((target.transform.position - this.transform.position).normalized * force);
	}
}