using UnityEngine;
using BossRush.Common;

public class PlayerMovement : MonoBehaviour {

	public float baseSpeed = 2.0f;
    private float speedMultiplier = 1.0f;  //Modify this to add slows/hastes to the player
    Rigidbody rb;
    RectTransform staminaBar;
    float barWidth;
    Transform modelTransform;

    Timer rollCooldown;
    Timer rollDuration;

    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 10;
        modelTransform = transform.Find("CharacterModel2");

        staminaBar = GameObject.Find("StaminaBar").GetComponent<RectTransform>();
        barWidth = staminaBar.sizeDelta.x;

        anim = gameObject.GetComponentInChildren<Animator>();
        rollDuration = new Timer(0.65f); // Length of roll clip
        rollCooldown = new Timer(1.0f); // Length of roll clip + a little buffer
    }

    // Update is called once per frame
    void Update () {
		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

		Vector3 direction = new Vector3(x, 0, y).normalized;
        ProcessRoll(direction);
        if(direction != Vector3.zero)
        {
            rb.AddForce(direction * GetSpeed(), ForceMode.VelocityChange);
            modelTransform.rotation = Quaternion.LookRotation(direction);
        }
        anim.SetBool("Running", direction != Vector3.zero);
    }

    void ProcessRoll(Vector3 direction)
    {
        //Keeps the player from rolling too much
        rollCooldown.update();
        rollDuration.update();

        bool rolling = anim.GetBool("Rolling");

        if (rolling && rollDuration.isReady())
        {
            anim.SetBool("Rolling", false);
        }

        Vector2 size = staminaBar.sizeDelta;
        size.x = rollCooldown.CurrentTime == 0 ? barWidth : barWidth * (1 - (rollCooldown.CurrentTime / rollCooldown.MaxTime));
        staminaBar.sizeDelta = size;

        //Start a roll if we can and the player wants to
        if (!rolling && rollCooldown.isReady() && Input.GetKeyDown(GameManager.RollKey) && anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            rollCooldown.reset();
            rollDuration.reset();
            anim.SetBool("Rolling", true);
        }
    }

    private float GetSpeed()
    {
        float speed;

        bool rolling = anim.GetBool("Rolling");

        //If currently rolling, override movement keys
        if (!rolling)
        {
            speed = baseSpeed;
        }
        else
        {
            speed = baseSpeed * 5 / 3;
        }

        //Applies slows/hastes to the player
        speed *= speedMultiplier;

        return speed;
    }
}