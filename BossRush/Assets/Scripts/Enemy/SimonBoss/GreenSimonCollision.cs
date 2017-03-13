using UnityEngine;
using System.Collections;

public class GreenSimonCollision : MonoBehaviour {
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
            simonScript.lightUpGreen();
            if (simonScript.waitingForInput == true)
            {
                simonScript.justPressed = 2;
                simonScript.checkInput();
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            simonScript.dimGreen();
        }
    }
}
