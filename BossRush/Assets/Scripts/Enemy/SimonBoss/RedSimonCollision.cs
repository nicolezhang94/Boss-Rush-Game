using UnityEngine;
using System.Collections;

public class RedSimonCollision : MonoBehaviour {

    public SimonScript simonScript;
    // Use this for initialization
    void Start()
    {
        simonScript = GetComponentInParent<SimonScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            simonScript.lightUpRed();
            if (simonScript.waitingForInput == true)
            {
                simonScript.justPressed = 1;
                simonScript.checkInput();
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            simonScript.dimRed();
        }
    }
}
