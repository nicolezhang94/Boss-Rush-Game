using UnityEngine;

public class HiveMovement : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public int rotationSpeed;
    public Transform spawnTarget;
    public SpawnBees spawnBees;
    public Vector3 target2;
    public Vector3 hiveTarget;
    public Vector3 dist;
    public EnemyHealth hiveHealth;
    AudioSource hiveSoundSource;
    public AudioClip hiveDeath;
    public int round = 1;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        spawnBees = GetComponent<SpawnBees>();
        target2 = new Vector3(2.16f, 3.5f, -9.2f);
        hiveHealth = GetComponent<EnemyHealth>();
        hiveSoundSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Destroy(gameObject);
            Die();
        }

        if (spawnBees.spawnState.Equals("doNothing"))
        {
            hiveTarget = target2;
        }
        if (spawnBees.spawnState.Equals("follow"))
        {
            hiveTarget = target.position;
        }
        dist = hiveTarget - transform.position;
        // if (dir != Vector3.zero)
        //  transform.rotation = Quaternion.Slerp(transform.rotation,
        //     Quaternion.FromToRotation(Vector3.right, dir),
        //   rotationSpeed * Time.deltaTime);
        if (dist.magnitude > 1 && spawnBees.spawnState != "waiting")
        //print(dist.magnitude);
        {//Move Towards Target
            transform.position += (hiveTarget - transform.position).normalized
            * moveSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, 4, transform.position.z);
        }
        if (spawnBees.spawnState.Equals("doNothing") && dist.magnitude <= 1)
        {
            print(dist.magnitude);
            spawnBees.inPosition = true;
            spawnBees.spawnState = "waiting";
            spawnBees.spawnPrevention = false;

        }
        if (spawnBees.inPosition == true && transform.childCount < 1 && spawnBees.spawnState.Equals("beesDead"))
        {
            if (round == 3)
            {
                Die();
            }
            else {
                spawnBees.spawnState = "follow";
                hiveHealth.health = 10;
                round++;
                moveSpeed = moveSpeed + 0.5f;
            }

        }
    }

    void Die()
    {
        //Do special things
        Debug.Log("Hive ded");
        GameObject.Find("Door").SetActive(false);
        //Do default things
        Destroy(gameObject);
        hiveHealth.Die();
    }
}