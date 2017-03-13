using UnityEngine;
using System.Collections;
using BossRush.Common;

public class ThirdOverworldLoader : MonoBehaviour
{
    private GameObject jewels;
    public GameObject eleDoor;
    public GameObject simonRock;
    public GameObject mainDoor;
    public GameObject eleBossZoneLoader;
    public GameObject simonBossZoneLoader;
    public GameObject finalBossZoneLoader;
    public GameObject player;


    // Use this for initialization
    void Start()
    {
        jewels = GameObject.Find("Jewels");

        eleDoor.SetActive(false);
        simonRock.SetActive(false);
        finalBossZoneLoader.SetActive(false);
        simonBossZoneLoader.SetActive(true);
        eleBossZoneLoader.SetActive(true);

        if (GameStateManager.GreenJewelBossDead)
            TurnOnGreenLights();
        if (GameStateManager.RedJewelBossDead)
            TurnOnRedLights();
        if (GameStateManager.RedJewelBossDead && GameStateManager.GreenJewelBossDead)
            OpenMainGate();

        switch (GameStateManager.PreviousScene)
        {
            case "ElementalBoss":
                player.transform.position = new Vector3(-27.93f, 0.319f, 13.5f);
                break;
            case "SimonBoss":
                player.transform.position = new Vector3(13.6f, 0.319f, 21.752f);
                break;
            default:
                break;
        }
    }

    public void TurnOnRedLights()
    {
        foreach(Transform jewel in jewels.transform)
        {
            if (jewel.gameObject.name.StartsWith("Red"))
            {
                Light redLight = jewel.gameObject.AddComponent<Light>();
                redLight.type = LightType.Point;
                redLight.color = Color.red;
                redLight.intensity = 1.0f;
            }
        }

        simonRock.SetActive(true);
        simonBossZoneLoader.SetActive(false);
    }

    public void TurnOnGreenLights()
    {
        foreach (Transform jewel in jewels.transform)
        {
            if (jewel.gameObject.name.StartsWith("Green"))
            {
                Light greenLight = jewel.gameObject.AddComponent<Light>();
                greenLight.type = LightType.Point;
                greenLight.color = Color.green;
                greenLight.intensity = 1.0f;
            }
        }

        eleDoor.SetActive(true);
        eleBossZoneLoader.SetActive(false);
    }

    public void OpenMainGate()
    {
        Destroy(mainDoor);
        finalBossZoneLoader.SetActive(true);
    }
}
