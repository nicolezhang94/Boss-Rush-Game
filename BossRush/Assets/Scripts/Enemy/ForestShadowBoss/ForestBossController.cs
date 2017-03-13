using UnityEngine;
using BossRush.Common;
using System.Collections.Generic;


public class ForestBossController : MonoBehaviour {

	List<int> lights = new List<int>();
	int lightIndex = 0;

	public Rigidbody bullet;
	public GameObject bossmesh;
	public GameObject player;
    public GameObject entrance;
	public float pPos;

    public AudioSource forestAudio;
    public AudioClip pewPew; 
    
    public EnemyHealth ForestBossHealth;

	public int shotcount = 3;

	public string bossstate = "waiting";
	public string roomstate = "dormant";

	Timer attackTime;
	public Light TLlight;
	public Light TRlight;
	public Light BLlight;
	public Light BRlight;
	public Light Mlight;
	Timer lightTime;
	Timer waitTime;
	Timer chargeTime;

	private Vector3[] p = new Vector3[12];

	Vector3 recoverPos = new Vector3(-10.55f, -4.5f, -16.33f);

	// Use this for initialization
	void Start () {
		p[0] = new Vector3 (-11.8f, 0.84f, -18.54f);
		p[1] = new Vector3 (-14.34f, 0.84f, -19.57f);
		p[2] = new Vector3 (-16.37f, 0.84f, -19.57f);
		p[3] = new Vector3 (-18.04f, 0.84f, -17.93f);
		p[4] = new Vector3 (-19.29f, 0.84f, -15.86f);
		p[5] = new Vector3 (-19.71f, 0.84f, -14.17f);
		p[6] = new Vector3 (-18.29f, 0.84f, -12.21f);
		p[7] = new Vector3 (-15.93f, 0.84f, -10.99f);
		p[8] = new Vector3 (-14.16f, 0.84f, -10.99f);
		p[9] = new Vector3 (-11.61f, 0.84f, -12.01f);
		p[10] = new Vector3 (-10.55f, 0.84f, -14.16f);
		p[11] = new Vector3 (-10.55f, 0.84f, -16.33f);

		for (int i = 0; i <= 50; i++) {
			int x = Random.Range(1,5);
			lights.Add(x);
		}

		TLlight.intensity = 0;
		TRlight.intensity = 0;
		BLlight.intensity = 0; 
		BRlight.intensity = 0; 
		Mlight.intensity  = 4;

		attackTime = new Timer (0.5f);
		waitTime = new Timer (3);
		lightTime = new Timer (1);
		chargeTime = new Timer (3);
	}

	void toggleLight (Light l) {
		if (l.intensity != 0) {
			l.intensity = 0;
		} else {
			l.intensity = 4;
		}
	}

	Light lightFromInt(int i) {
		if (i == 1)
			return TLlight;
		if (i == 2)
			return TRlight;
		if (i == 3)
			return BRlight;
		if (i == 4)
			return BLlight;
		return TLlight;
	}
	
	// Update is called once per frame
	void Update () {
		pPos = player.transform.position.x;

		Debug.Log (player.transform.position.x);

        if (Input.GetKeyDown(KeyCode.X))
        {
            Die();
        }

		if (roomstate == "dormant") {
			if (pPos > -18) {
				Debug.Log ("STARTING");
				Mlight.intensity = 0;
				toggleLight (lightFromInt (lights [lightIndex]));
				roomstate = "active";
			}
		}

		if (roomstate == "active") {
			// Rotatng lights
			lightTime.update ();
			if (lightTime.isReady ()) {
				toggleLight (lightFromInt (lights [lightIndex]));
				lightIndex++;
				if (lightIndex == lights.Count)
					lightIndex = 0;
				toggleLight (lightFromInt (lights [lightIndex]));
				lightTime.reset ();
			}

			if (bossstate == "attacking") {
				attackTime.update ();
				if (attackTime.isReady ()) {
					attackTime.reset ();
					Rigidbody bulletClone = (Rigidbody) Instantiate(bullet, bossmesh.transform.position, Quaternion.identity);
                    forestAudio.PlayOneShot(pewPew);
					bulletClone.AddForce((player.transform.position - bossmesh.transform.position) * .001f * Time.smoothDeltaTime);
					shotcount--;

					if (shotcount == 0) {
						attackTime.reset ();
						bossstate = "waiting";
						waitTime.reset ();
						bossmesh.transform.position = recoverPos;
						shotcount = 3;
					}
				}
			}

			if (bossstate == "waiting") {
				waitTime.update ();
				if (waitTime.isReady ()) {
					int x = Random.Range(0,12);
					Debug.Log (x);
					bossmesh.transform.position = p [x];
					chargeTime.reset ();
					bossstate = "charging";
				}
			}

			if (bossstate == "charging") { 
				chargeTime.update ();
				//Debug.Log ("CHARGE");
				if (chargeTime.isReady ()) {
					bossstate = "attacking";
					chargeTime.reset ();
				}
			}
		}
	}
    
    void Die()
    {
        entrance.SetActive(true);
        GameStateManager.GreenShieldBossDead = true;
        Destroy(bossmesh);
        Destroy(gameObject);
    }
}
