using UnityEngine;
using System.Collections;

public class YellowSimonCollision : MonoBehaviour {
    public SimonScript simonScript;
	// Use this for initialization
	void Start () {
        simonScript = GetComponentInParent<SimonScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            simonScript.lightUpYellow();
            if(simonScript.waitingForInput == true)
            {
                simonScript.justPressed = 4;
                simonScript.checkInput();
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            simonScript.dimYellow();
        }
    }
}
