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
            foreach(Hand hand in controller.Frame().Hands)
            {
                this.transform.position = controller.Frame().Hands[0].PalmPosition.ToVector3();
                this.transform.rotation = Quaternion.LookRotation(controller.Frame().Hands[0].PalmPosition.ToVector3(), Vector3.up);
            }
        }
        else if(!fire.isPaused)
        {
            fire.Pause();
        }
	}
}
