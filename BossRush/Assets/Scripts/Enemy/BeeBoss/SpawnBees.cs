using UnityEngine;

public class SpawnBees : MonoBehaviour
{
    //public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject Bee;                // The enemy prefab to be spawned.
    public int numberOfBees;
    public float spawnTime = 3f;            // How long between each spawn.
    public Vector3[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public GameObject Hive;
    public EnemyHealth hiveHealth;
    public BoidController boidController;
    public bool spawnPrevention = false;
    public string spawnState = "follow";
    public GameObject[] beeList;
    public bool allBeesGone = true;
    public bool inPosition = false;
    public int spawnCount = 0;
    void Start()
    {
        hiveHealth = GetComponent<EnemyHealth>();
        boidController = GetComponent<BoidController>();
    }

    void Spawn()
    {
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        
        for (int i = 0; i < numberOfBees; i++)
        {
            Vector3 position = new Vector3(Random.Range(-10.0F, 10.0F), 0, Random.Range(-10.0F, 10.0F));
            Instantiate(Bee, transform.position + position, transform.rotation);
        }
        spawnCount++;
    }

    void CheckNumBees()
    {
        beeList = GameObject.FindGameObjectsWithTag("Bee");
        if (beeList.Length < 5)
        {
            allBeesGone = true;
            if (spawnState.Equals("waiting"))
            {
                spawnState = "beesDead";
            }
        }
    }

    void CheckState()
    {
        if (spawnState.Equals("waiting"))
        {
        }
    }

    void Update()
    {

        if (hiveHealth.health <= 0f && spawnState.Equals("follow"))
        {
            if (spawnCount > 1)
            {
                //deathAnimation?
                Destroy(Hive);
            }
            spawnState = "doNothing";

        }
        if (spawnPrevention == false && inPosition == true && spawnState.Equals("waiting"))
        {

            boidController.Spawn();
            spawnPrevention = true;
            allBeesGone = false;

        }
        CheckNumBees();
    }
}