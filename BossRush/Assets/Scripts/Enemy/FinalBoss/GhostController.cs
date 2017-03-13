using UnityEngine;
using System.Collections.Generic;
using BossRush.Common;

public class GhostController : MonoBehaviour
{
    public List<LastBossAction> Actions = null;
    GhostSword sword;
    Animator anim;
    Transform modelTransform;

    #region Stuff from PlayerMovement
    public float baseSpeed = 2.0f;
    private float speedMultiplier = 1.0f;  //Modify this to add slows/hastes to the player
    Rigidbody rb;

    Timer rollCooldown;
    Timer rollDuration;
    Vector3 direction = new Vector3();
    #endregion

    // Use this for initialization
    void Awake()
    {
        if (Actions == null)
        {
            Destroy(gameObject);
        }

        rollDuration = new Timer(0.75f);
        rollCooldown = new Timer(1.0f);
    }

    void Start()
    {
        sword = GetComponentInChildren<GhostSword>();
        rb = GetComponent<Rigidbody>();
        rb.drag = 10;

        modelTransform = transform.Find("CharacterModel2");
        anim = gameObject.GetComponentInChildren<Animator>();

        var ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        var enemies = GameObject.Find("Final Boss").transform;
        var player = GameObject.FindGameObjectWithTag("Player");
        var collider = GetComponent<Collider>();

        foreach(var ghost in ghosts)
        {
            Physics.IgnoreCollision(collider, ghost.GetComponent<BoxCollider>());
        }

        foreach (Transform enemy in enemies)
        {
            Physics.IgnoreCollision(collider, enemy.gameObject.GetComponent<Collider>());
        }

        Physics.IgnoreCollision(collider, player.GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        ProcessGhostData();
        ProcessRoll(direction);
        rb.AddForce(direction * GetSpeed(), ForceMode.VelocityChange);
        if(direction != Vector3.zero)
        {
            modelTransform.rotation = Quaternion.LookRotation(direction);
        }
        anim.SetBool("Running", direction != Vector3.zero);
    }

    private void RotateModel()
    {
        if (direction == Vector3.forward)
        {
            modelTransform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (direction == Vector3.right)
        {
            modelTransform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (direction == Vector3.back)
        {
            modelTransform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (direction == Vector3.left)
        {
            modelTransform.localEulerAngles = new Vector3(0, 270, 0);
        }
        else if (direction == (Vector3.right + Vector3.forward).normalized)
        {
            modelTransform.localEulerAngles = new Vector3(0, 45, 0);
        }
        else if (direction == (Vector3.right + Vector3.back).normalized)
        {
            modelTransform.localEulerAngles = new Vector3(0, 135, 0);
        }
        else if (direction == (Vector3.left + Vector3.back).normalized)
        {
            modelTransform.localEulerAngles = new Vector3(0, 225, 0);
        }
        else if (direction == (Vector3.left + Vector3.forward).normalized)
        {
            modelTransform.localEulerAngles = new Vector3(0, 315, 0);
        }
    }

    private void ProcessGhostData()
    {
        if (Actions.Count > 0)
        {
            DoAction(Actions[0]);
        }
        else
        {
            Die();
        }
    }

    private void DoAction(LastBossAction action)
    {
        float x = 0f;
        float y = 0f;
        float tolerance = 0.1f;

        if (action.Time <= Time.timeSinceLevelLoad + tolerance)
        {
            if (FlagHelper.IsSet(action.Mask, FinalBossFlag.W))
            {
                y += 1;
            }
            if (FlagHelper.IsSet(action.Mask, FinalBossFlag.A))
            {
                x -= 1;
            }
            if (FlagHelper.IsSet(action.Mask, FinalBossFlag.S))
            {
                y -= 1;
            }
            if (FlagHelper.IsSet(action.Mask, FinalBossFlag.D))
            {
                x += 1;
            }
            if (FlagHelper.IsSet(action.Mask, FinalBossFlag.Roll))
            {
                anim.SetBool("Rolling", true);
                rollCooldown.reset();
                rollDuration.reset();
            }
            if (FlagHelper.IsSet(action.Mask, FinalBossFlag.Attack))
            {
                sword.Swing();
            }
            if (FlagHelper.IsSet(action.Mask, FinalBossFlag.Die))
            {
                Die();
            }

            Actions.RemoveAt(0);
        }

        direction = new Vector3(x, 0, y).normalized;
    }

    #region Tweaked stuff from PlayerMovement
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
    #endregion

    void Die()
    {
        Destroy(gameObject);
    }
}
