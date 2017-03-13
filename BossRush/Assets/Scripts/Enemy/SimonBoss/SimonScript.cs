using UnityEngine;
using System.Collections;
using BossRush.Common;

public class SimonScript : MonoBehaviour {
    ArrayList pattern = new ArrayList();
    public int currentLevel = 0;
    public int currentMaxLevel;
    public int levelToCheck = 0;
    public bool displayingPattern = false;
    public bool waitingForInput = false;
    public bool makeItGlowRed = false;
    public bool busy = false;
    public bool firstTimeReset = true;
    public bool lastRound;
    public bool notePlaying = false;
    public bool spawningBees = true;

    public Light spotLight;
    public int currentRound;
    public int numberOfRounds;
    public GameObject greenSimon;
    public GameObject redSimon;
    public GameObject blueSimon;
    public GameObject yellowSimon;
    public GameObject Bee;
    public Material BlueGlow;
    public Material BlueMatte;
    public Material YellowGlow;
    public Material YellowMatte;
    public Material RedGlow;
    public Material RedMatte;
    public Material GreenGlow;
    public Material GreenMatte;
    Vector3 beeLocation;
    Vector3 beeLocation2;
    public string currentSimonSquare;
    public int justPressed;
    ArrayList spawns = new ArrayList();
    Timer glowTime;
    Timer waitTime;
    Timer startingTime;
    Timer beeSpawn;

    public Transform beeLoc;
    public AudioClip blueSound;
    public AudioClip redSound;
    public AudioClip greenSound;
    public AudioClip yellowSound;
    public AudioClip loseSound;
    AudioSource simonAudio;
    // Use this for initialization
    void Start () {
        generatePattern(16);
        currentRound = 1;
        numberOfRounds = 4;
        glowTime = new Timer(1);
        waitTime = new Timer(2);
        startingTime = new Timer(5);
        glowTime.reset();
        startingTime.reset();
        currentMaxLevel = 3;
        currentSimonSquare = "none";
        beeSpawn = new Timer(10);
        beeSpawn.reset();
        beeLocation = new Vector3(-5, -2, -1);
        beeLocation2 = new Vector3(-8, -2, -1);
        spawns.Add(beeLocation);
        spawns.Add(beeLocation2);
        simonAudio = GetComponent<AudioSource>();
        
    }
	// Generate simon pattern
    // wait a certain amount of time
    // Play the first pattern (index 0 of pattern)
    // Player steps on right one
    // Wait
    // Next pattern (index 0 + 1)
    // Make sure player does it right
    // Wait
    // etc. etc.
	// Update is called once per frame
	void Update () {
        glowTime.update();
        waitTime.update();
        startingTime.update();
        beeSpawn.update();
        if (Input.GetKeyDown(KeyCode.X))
        {
            Win();
        }
        if (startingTime.isReady())
        {
            displayingPattern = true;
        }
        if(beeSpawn.isReady() && spawningBees == true)
        {
            
            Instantiate(Bee, beeLocation, transform.localRotation);
            beeSpawn.reset();
        }
        if (displayingPattern == true)
        {
            spotLight.intensity = 0;
            startingTime.reset();
            if (firstTimeReset == true)
            {
                glowTime.reset();
                waitTime.reset();
                firstTimeReset = false;
            }
            if (busy == false)
            {
                displayNext(currentLevel);
                if (glowTime.isReady())
                {
                    dimNext(currentLevel);
                    if(waitTime.isReady())
                    {
                        if (currentLevel < currentMaxLevel)
                        {
                            notePlaying = false;
                            currentLevel++;
                            glowTime.reset();
                            waitTime.reset();
                        }
                        else
                        {
                            displayingPattern = false;
                            waitingForInput = true;
                            notePlaying = false;
                        }
                    }
                }
            }
        }
        if (waitingForInput == true)
        {
            startingTime.reset();
            spotLight.intensity = 2;
        }
    }

    void generatePattern(int amount)
    {   
        for(int i=0; i< amount; i++)
        {
            pattern.Add(Random.Range(1, 4));
        }
    }

    void displayNext(int index)
    {
        if (pattern[index].Equals(1))
        {
            lightUpRed();
        }
        if (pattern[index].Equals(2))
        {
            lightUpGreen();
        }
        if (pattern[index].Equals(3))
        {
            lightUpBlue();
        }
        if (pattern[index].Equals(4))
        {
            lightUpYellow();
        }
    }

    void dimNext(int index)
    {
        if (pattern[index].Equals(1))
        {
            dimRed();
        }
        if (pattern[index].Equals(2))
        {
            dimGreen();
        }
        if (pattern[index].Equals(3))
        {
            dimBlue();
        }
        if (pattern[index].Equals(4))
        {
            dimYellow();
        }
    }

    void displayPattern(int level)
    {
        displayingPattern = true;
        for(int i=0; i<level; i++)
        {
            //while(!currentSimonSquare.Equals("none"))
            //{
            //    print("waiting");
            //}
            if (pattern[i].Equals(1))
            {
               
                lightUpRed();
            }
            if (pattern[i].Equals(2))
            {
                print("Should Light up green");
                lightUpGreen();
            }
            if (pattern[i].Equals(3))
            {
                print("Should Light up blue");
                lightUpBlue();
            }
            if (pattern[i].Equals(4))
            {
                print("Should Light up yellow");
                lightUpYellow();
            }
        }
    }

    public void lightUpRed()
    {
        redSimon.GetComponent<Renderer>().material = RedGlow;
        if (notePlaying == false)
        {
            simonAudio.PlayOneShot(redSound);
            notePlaying = true;
        }
    }

    public void dimRed()
    {
        redSimon.GetComponent<Renderer>().material = RedMatte;
        currentSimonSquare = "none";
        busy = false;
    }
    public void lightUpGreen()
    {
        greenSimon.GetComponent<Renderer>().material = GreenGlow;
        if (notePlaying == false)
        {
            simonAudio.PlayOneShot(greenSound);
            notePlaying = true;
        }
    }

    public void dimGreen()
    {
        greenSimon.GetComponent<Renderer>().material = GreenMatte;
        currentSimonSquare = "none";
        busy = false;
    }

    public void lightUpBlue()
    {
        blueSimon.GetComponent<Renderer>().material = BlueGlow;
        
        if (notePlaying == false)
        {
            simonAudio.PlayOneShot(blueSound);
            notePlaying = true;
        }
    }

    public void dimBlue()
    {
        blueSimon.GetComponent<Renderer>().material = BlueMatte;
        currentSimonSquare = "none";
        busy = false;
    }

    public void lightUpYellow()
    {
        yellowSimon.GetComponent<Renderer>().material = YellowGlow;
        
        if (notePlaying == false)
        {
            simonAudio.PlayOneShot(yellowSound);
            notePlaying = true;
        }
    }

    public void dimYellow()
    {
            yellowSimon.GetComponent<Renderer>().material = YellowMatte;
            currentSimonSquare = "none";
            busy = false;
    }

   public void checkInput()
    {
        notePlaying = false;
        int x = (int)pattern[levelToCheck];
        if(justPressed == x)
        {
            levelToCheck++;
        }
        if(justPressed != x)
        {
            notePlaying = false;
            levelToCheck = 0;
            currentLevel = 0;
            waitingForInput = false;
            firstTimeReset = true;
            simonAudio.PlayOneShot(loseSound);
            
        }
        if(levelToCheck > currentLevel)
        {
            if(lastRound == true)
            {
                Win();
            }
            else
            {
                waitingForInput = false;
                currentLevel = 0;
                levelToCheck = 0;
                currentMaxLevel++;
                currentRound++;
                firstTimeReset = true;
                if (currentRound == numberOfRounds)
                {
                    lastRound = true;
                    currentMaxLevel = currentMaxLevel + 3;
                }
            }
            
        }
        
    }
    public void Win()
    {
        GameStateManager.RedJewelBossDead = true;
        spawningBees = false;
        GameObject.Find("Door1").SetActive(false);
        GameObject.Find("Door2").SetActive(false);
    }

}
