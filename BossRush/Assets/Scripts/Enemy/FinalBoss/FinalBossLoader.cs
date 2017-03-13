using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BossRush.Common;

public class FinalBossLoader : MonoBehaviour
{
    public GameObject Ghost;
    public GameObject Player;

    // Use this for initialization
    void Awake()
    {
        GetGhostData();
        Destroy(this);
    }

    private void GetGhostData()
    {
        var path = Application.persistentDataPath + GameManager.FinalBossFileName;
        List<List<LastBossAction>> ghosts = new List<List<LastBossAction>>();

        using (var fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            var bf = new BinaryFormatter();
            while (fs.Position != fs.Length)
            {
                ghosts.Add((List<LastBossAction>)bf.Deserialize(fs));
            }

            if (ghosts.Count > 10)
            {
                ghosts = new List<List<LastBossAction>>(ghosts.GetRange(ghosts.Count - 10, 10));
            }

        }

        foreach (var ghost in ghosts)
        {
            var clone = Instantiate(Ghost, Player.GetComponent<Transform>().position, Player.GetComponent<Transform>().rotation);
            if(ghost.Count > 1 && ghost[0].Time > ghost[1].Time)
            {
                ghost.RemoveAt(0); //Hack hack hack hack
            }
            ((GameObject)clone).GetComponent<GhostController>().Actions = ghost;
        }
    }
}
