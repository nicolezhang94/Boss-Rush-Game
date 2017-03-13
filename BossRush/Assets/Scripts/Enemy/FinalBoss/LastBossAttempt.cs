using UnityEngine;
using BossRush.Common;

public class LastBossAttempt : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }
    
    void Update()
    {
        AddToAttempt();
    }

    public void AddToAttempt()
    {
        FinalBossFlag mask = FinalBossFlag.None;

        if (Input.GetKey(KeyCode.W))
        {
            mask = mask | FinalBossFlag.W;
        }
        if (Input.GetKey(KeyCode.A))
        {
            mask = mask | FinalBossFlag.A;
        }
        if (Input.GetKey(KeyCode.S))
        {
            mask = mask | FinalBossFlag.S;
        }
        if (Input.GetKey(KeyCode.D))
        {
            mask = mask | FinalBossFlag.D;
        }
        if (Input.GetKey(GameManager.RollKey))
        {
            mask = mask | FinalBossFlag.Roll;
        }
        if (Input.GetKey(GameManager.AttackKey) || Input.GetMouseButtonUp(0))
        {
            mask = mask | FinalBossFlag.Attack;
        }

        if (mask != FinalBossFlag.None)
        {
            GameManager.FinalBossAttempt.Add(new LastBossAction(Time.timeSinceLevelLoad, mask));
        }
    }
}
