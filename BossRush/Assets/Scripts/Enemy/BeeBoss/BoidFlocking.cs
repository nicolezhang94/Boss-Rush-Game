using UnityEngine;
using System.Collections;

public class BoidFlocking : MonoBehaviour
{
    private GameObject Controller;
    private bool inited = false;
    private float minVelocity;
    private float maxVelocity;
    private float randomness;
    public float centerVariable;
    public float velocityVariable;
    public float followVariable;
    public Transform chasee;
    void Start()
    {
        chasee  = GameObject.Find("Player").transform;
        StartCoroutine("BoidSteering");

    }
     void update()
    {
       // GetComponent<Rigidbody>().position = new Vector3(transform.position.x, 4, transform.position.z);
    }

    IEnumerator BoidSteering()
    {
        while (true)
        {
            if (inited)
            {
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + Calc() * Time.deltaTime;

                // enforce minimum and maximum speeds for the boids
                float speed = GetComponent<Rigidbody>().velocity.magnitude;
                if (speed > maxVelocity)
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxVelocity;
                }
                else if (speed < minVelocity)
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * minVelocity;
                }
            }

            float waitTime = Random.Range(0.3f, 0.5f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private Vector3 Calc()
    {
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);

        randomize.Normalize();
        BoidController boidController = Controller.GetComponent<BoidController>();
        Vector3 flockCenter = boidController.flockCenter;
        Vector3 flockVelocity = boidController.flockVelocity;
        Vector3 follow = chasee.position;

        flockCenter = flockCenter - transform.position;
        flockVelocity = flockVelocity - GetComponent<Rigidbody>().velocity;
        transform.position = new Vector3(transform.position.x, 4, transform.position.z);
        follow = follow - transform.position;

        return (flockCenter*centerVariable + flockVelocity *velocityVariable  + follow *followVariable + randomize * randomness);
    }

    public void SetController(GameObject theController)
    {
        Controller = theController;
        BoidController boidController = Controller.GetComponent<BoidController>();
        minVelocity = boidController.minVelocity;
        maxVelocity = boidController.maxVelocity;
        randomness = boidController.randomness;
        
        inited = true;
    }
}