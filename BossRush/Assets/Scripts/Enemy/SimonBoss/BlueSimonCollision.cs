using UnityEngine;
using System.Collections;

public class BlueSimonCollision : MonoBehaviour {
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
            simonScript.lightUpBlue();
            if (simonScript.waitingForInput == true)
            {
                simonScript.justPressed = 3;
                simonScript.checkInput();
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            simonScript.dimBlue();
        }
    }
}
