using UnityEngine;
using System.Collections;
using Leap;

public class FireballController : MonoBehaviour {

    public GameObject gestureManager;
    private Controller controller;

	// Use this for initialization
	void Start () {
        this.GetComponent<ParticleSystem>().Pause();
        controller = new Controller();
	}
	
	// Update is called once per frame
	void Update () {
        ParticleSystem fire = this.GetComponent<ParticleSystem>();
        if (gestureManager.GetComponent<GestureManager>().getCurrentGesture().Equals("Flamethrower"))
        {
            if(!fire.isPlaying)
            {
                fire.Play();
            }
        }
        else if(!fire.isPaused)
        {
            fire.Pause();
        }
	}
}
