using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    public Transform target;
    public int moveSpeed;
    public int rotationSpeed;
    public EnemyHealth BeeHealth;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        moveSpeed = Random.Range(1, 4);
    }

    void OnCollisionEnter(Collision col)
    {
        //if(col.gameObject.name == "Sword")
        //{
        //    BeeHealth.takeDamage();
        //}
        //if (col.gameObject.name == "Player")
        //{
        //    Player.takeDamage();
        //}

    }

    void Update()
    {

        Vector3 dist = target.position - transform.position;
        dist.y = 1.0f;
        //if (dir != Vector3.zero)
        //    transform.rotation = Quaternion.Slerp(transform.rotation,
        //       Quaternion.FromToRotation(Vector3.right, dir),
        //     rotationSpeed * Time.deltaTime);

        if (dist.magnitude > .1)
        {
            //Move Towards Target
            transform.position += (target.position - transform.position).normalized
                * moveSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        }
    }

    void Die()
    {
        //Do special things
        Debug.Log("Bee ded");

        //Do default things
        BeeHealth.Die();
    }
}