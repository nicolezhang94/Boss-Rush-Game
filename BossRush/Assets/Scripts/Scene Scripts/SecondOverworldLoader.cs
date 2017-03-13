using UnityEngine;
using System.Collections;
using BossRush.Common;

public class SecondOverworldLoader : MonoBehaviour
{
    private GameObject greenShield;
    private GameObject redShield;
    private GameObject greenGateShield;
    private GameObject redGateShield;
    public GameObject blockTree;
    public GameObject blockCliff;
    public GameObject gateZoneLoader;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        greenShield = GameObject.Find("GreenShield");
        redShield = GameObject.Find("RedShield");
        greenGateShield = GameObject.Find("GreenGateShield");
        redGateShield = GameObject.Find("RedGateShield");

        blockTree.SetActive(false);
        blockCliff.SetActive(false);
        gateZoneLoader.SetActive(false);

        if (GameStateManager.GreenShieldBossDead)
            TurnOnGreenLights();
        if (GameStateManager.RedShieldBossDead)
            TurnOnRedLights();
        if (GameStateManager.RedShieldBossDead && GameStateManager.GreenShieldBossDead)
            OpenMainGate();

        switch (GameStateManager.PreviousScene)
        {
            case "ForestBoss":
                player.transform.position = new Vector3(25.69f, 0.319f, 25.06f);
                break;
            case "APIBoss":
                player.transform.position = new Vector3(-10.39f, 0.319f, 23.2f);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOnRedLights()
    {
        Light redLight = redShield.AddComponent<Light>();
        redLight.type = LightType.Point;
        redLight.color = Color.red;
        redLight.intensity = 2.0f;

        redLight = redGateShield.AddComponent<Light>();
        redLight.type = LightType.Point;
        redLight.color = Color.red;
        redLight.intensity = 2.0f;

        blockCliff.SetActive(true);
    }

    public void TurnOnGreenLights()
    {
        Light greenLight = greenShield.AddComponent<Light>();
        greenLight.type = LightType.Point;
        greenLight.color = Color.green;
        greenLight.intensity = 2.0f;

        greenLight = greenGateShield.AddComponent<Light>();
        greenLight.type = LightType.Point;
        greenLight.color = Color.green;
        greenLight.intensity = 2.0f;

        blockTree.SetActive(true);
    }

    public void OpenMainGate()
    {
        Destroy(GameObject.Find("IronDoor"));
        gateZoneLoader.SetActive(true);
    }
}
