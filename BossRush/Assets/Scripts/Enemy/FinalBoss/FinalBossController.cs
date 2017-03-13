using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using BossRush.Common;
using System.IO;
using UnityEngine.UI;

public class FinalBossController : MonoBehaviour
{
    private Vector3[,] grid = new Vector3[9,9];
    public GameObject clone;
    private List<FinalBossSegment> segs;
    private List<Vector3> targets;
    int targetX, targetY;
    bool movingX, movingY;
    public float speed;
    EnemyHealth currentEH;
    public GameObject text;

    // Use this for initialization
    void Start()
    {
        SetUpGrid();
        segs = new List<FinalBossSegment>();
        targets = new List<Vector3>();
        targetX = 4;
        targetY = 0;
        targets.Add(grid[targetX, targetY]);
        movingY = true;
        foreach (Transform child in transform)
        {
            segs.Add(new FinalBossSegment(child, targets[0]));
        }
        ChangeKillableSegment();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            for(int i = 0; i < segs.Count; i++)
            {
                Die();
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            DeleteGhostFile();
        }

        if(currentEH.health < 1)
        {
            Die();
        }

        MoveSpheres();
    }

    void MoveSpheres()
    {
        for(int i = 0; i < segs.Count; i++)
        {
            if (segs[i].HasReachedGoal())
            {
                if (i == 0)
                {
                    GetNextGoal();
                }
                else
                {
                    segs[i].goal = targets[targets.LastIndexOf(segs[i].goal) + 1];
                }
            }

            segs[i].transform.position = Vector3.MoveTowards(segs[i].transform.position, segs[i].goal, speed * Time.deltaTime);
        }
    }

    void GetNextGoal()
    {
        if (movingX)
        {
            movingX = false;
            movingY = true;
            var range = Random.Range(0, 9);
            while(range == targetY)
            {
                range = Random.Range(0, 9);
            }
            targetY = range;
        }
        else
        {
            movingY = false;
            movingX = true;
            var range = Random.Range(0, 9);
            while (range == targetX)
            {
                range = Random.Range(0, 9);
            }
            targetX = range;
        }

        targets.Add(grid[targetX, targetY]);
        segs[0].goal = grid[targetX, targetY];
    }

    void SetUpGrid()
    {
        float startX = 8.5f;
        float startZ = -1f;
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                grid[i, j] = new Vector3(startX + i * 7f, 1, startZ + j * 7f);
            }
        }
    }

    void Die()
    {
        if(segs.Count == 1)
        {
            Destroy(gameObject);
            Time.timeScale = 0;
            text.SetActive(true);
        }
        else
        {
            segs.RemoveAt(segs.Count - 1);
            currentEH.Die();
            ChangeKillableSegment();
        }
    }

    void ChangeKillableSegment()
    {
        var last = segs[segs.Count - 1];
        currentEH = last.transform.gameObject.AddComponent<EnemyHealth>();
        currentEH.health = 1;
        last.transform.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 0.65f, 0f);
        last.transform.localScale = new Vector3(2f, 2f, 2f);
        last.transform.gameObject.tag = "Hostile";
    }

    void DeleteGhostFile()
    {
        File.Delete(Application.persistentDataPath + GameManager.FinalBossFileName);
        SceneManager.LoadScene("FinalBoss");
    }
}
