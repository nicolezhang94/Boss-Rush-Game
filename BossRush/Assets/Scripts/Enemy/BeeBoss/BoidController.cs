using UnityEngine;

public class BoidController : MonoBehaviour
{
    public float minVelocity = 1;
    public float maxVelocity = 2;
    public float randomness = 1;
    public int flockSize = 50;
    public GameObject Bee;

    public Vector3 flockCenter;
    public Vector3 flockVelocity;

    public GameObject[] bees;

    public void Spawn()
    {
        bees = new GameObject[flockSize];
        for (var i = 0; i < flockSize; i++)
        {
            
            GameObject bee = Instantiate(Bee, transform.position, transform.rotation) as GameObject;
            bee.transform.parent = transform;
            bee.transform.position = transform.position;
            bee.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)), ForceMode.Force);
            bee.GetComponent<BoidFlocking>().SetController(gameObject);
            bees[i] = bee;
        }
    }

    void Update()
    {
        Vector3 theCenter = Vector3.zero;
        Vector3 theVelocity = Vector3.zero;
        if(bees != null)
        {
            foreach (GameObject bee in bees)
            {
                theCenter = theCenter + bee.transform.position;
                theVelocity = theVelocity + bee.GetComponent<Rigidbody>().velocity;
            }
        }
        

        flockCenter = theCenter / (flockSize);
        flockVelocity = theVelocity / (flockSize);
    }
}