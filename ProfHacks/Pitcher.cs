using UnityEngine;
using System.Collections;

public class Pitcher : MonoBehaviour {

    public GameObject spawnBall;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("p"))
        {
            Instantiate(spawnBall);
        }
	}
}
