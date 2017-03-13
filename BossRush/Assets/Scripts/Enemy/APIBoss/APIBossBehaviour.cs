using UnityEngine;
using System.Collections;
using SimpleJSON;
using BossRush.Common;

public class APIBossBehaviour : MonoBehaviour {

	public GameObject Head;
	public GameObject LeftFist;
	public GameObject RightFist;
	public EnemyHealth APIBossHealth;
	public GameObject Player;
    public GameObject door1;
    public GameObject door2;
    public GameObject entrance;
	public float attackPower = 0;
	public float speed = 0;
	float temp = 0.0f;
	public float distance;

	public int state = 1; //Follow, punch
	public float spinRad = 2.0f;
	public int followPunchCount = 0;
	Vector3 AORot = Vector3.up;
	Vector3 punchTarget;// = new Vector3();

	Vector3 leftfiststartpos;
	Vector3 rightfiststartpos;

	float spinspeed = 600.0f;

	Timer chaseTimer = new Timer(10); // how long the boss will chase
	Timer restTimer = new Timer(5); // how long the boss will rest
	Timer spinTimer = new Timer(3); // Time of the spin attack

	float pickFist = 0;


	string url = "api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&APPID=293aaa3d61914cf99b50d6cf66586d5a";
	string ipurl = "http://ip-api.com/json";

	WWW www;

	public IEnumerator getIPAndWeather()
	{
		yield return www;
		if(www.error == null)
		{
			var json = JSON.Parse(www.text);
			url = string.Format(url, json["lat"].AsFloat, json["lon"].AsFloat);
		}
		else
		{
			url = string.Format(url, "44.9777530", "-93.2650110");
			Debug.Log("WWW Error; using default location: " + www.error);
		}

		www = new WWW(url);
		StartCoroutine(getW());
	}

	public IEnumerator getW() {
		yield return www;
		if (www.error == null)
		{
			var json = JSON.Parse(www.text);
			temp = json["main"]["temp"].AsFloat;
			temp = (float)(temp * (9.0f/5.0f)) - 459.67f;

			temp = Mathf.Max (temp, -50);//Trim to reasonable temps
			temp = Mathf.Min (temp, 120);

			Debug.Log (temp);

			temp += 50; // Zero it out

			// Higher temp = higher speed and lower attack power
			speed = (100.0f / 170.0f) * temp; //Speed mapped to 0-100
			attackPower = 100 - speed;  //Attack power is inverse of attackPower

		} else {
			Debug.Log("WWW Error: "+ www.error);
		}
	}

	// Use this for initialization
	void Start () {
		www = new WWW(ipurl);
		StartCoroutine(getIPAndWeather());
		leftfiststartpos = LeftFist.transform.position;
		rightfiststartpos = RightFist.transform.position;
		punchTarget = new Vector3(1,1,1);
		restTimer.reset ();
		chaseTimer.reset ();
		spinTimer.reset ();
	}

	// Update is called once per frame
	void Update () {
		if(APIBossHealth.IsDead() || Input.GetKeyDown(KeyCode.X)) { Die(); }

        distance = (Player.transform.position - Head.transform.position).magnitude;

		if (state == 1) {
			chaseTimer.update ();
			Debug.Log (chaseTimer.CurrentTime);
			Debug.Log ("following");
			Vector3 look = Head.transform.position - Player.transform.position;
			look.y = 0;
			var rot = Quaternion.LookRotation (look);
			Head.transform.rotation = Quaternion.Slerp (Head.transform.rotation, rot, Time.deltaTime * 2.0f * (1 + speed / 100.0f));
				Head.transform.position = Vector3.MoveTowards (Head.transform.position, Player.transform.position, 0.05f * (1 + speed / 100.0f));
			if (distance < 1.4) {
				state = 2;
				leftfiststartpos = LeftFist.transform.position;
				rightfiststartpos = RightFist.transform.position;
				punchTarget = Player.transform.position;
				pickFist = Random.Range (0, 10);
			}
			if (chaseTimer.isReady()) {
				state = 3;
			}
		}
		if (state == 2) {

			if (pickFist > 5)
				LeftFist.transform.position = Vector3.Lerp (LeftFist.transform.position, punchTarget, 0.1f);

			if (Mathf.Abs (LeftFist.transform.position.x - punchTarget.x) < 0.02 && Mathf.Abs (LeftFist.transform.position.y - punchTarget.y) < 0.02) {
				LeftFist.transform.position = leftfiststartpos;
				state = 1;
			} else if (pickFist < 5) {
				RightFist.transform.position = Vector3.Lerp (RightFist.transform.position, punchTarget, 0.1f);


				if (Mathf.Abs (RightFist.transform.position.x - punchTarget.x) < 0.02 && Mathf.Abs (RightFist.transform.position.z - punchTarget.z) < 0.02) {
					
					if (Mathf.Abs (RightFist.transform.position.x - punchTarget.x) < 0.02 && Mathf.Abs (RightFist.transform.position.y - punchTarget.y) < 0.02) {
						RightFist.transform.position = rightfiststartpos;
						state = 1;
					}
				}
			}
		}
		if (state == 3) { // return to middle
			Debug.Log("Returning");
			Head.transform.position = Vector3.Lerp (Head.transform.position, new Vector3(-.16f, 0.15f, 0.0f), 0.1f * (1 + speed / 100.0f));

			if (Mathf.Abs (Head.transform.position.x - -.16f) < 0.02 && Mathf.Abs (Head.transform.position.z - 0.0f) < 0.02) {
				state = 4;
				restTimer.reset ();
			}
		}
		if (state == 4) { // rest
			Debug.Log("Resting");
			restTimer.update();

			if (restTimer.isReady()) {
				if (Random.Range (0, 10) > 2) {
					state = 1;
					chaseTimer.reset ();
				} else {
					spinTimer.reset ();
					state = 5;
					leftfiststartpos = LeftFist.transform.position;
					rightfiststartpos = RightFist.transform.position;
				}
			}
		}
		if (state == 5) { // spin attack
			spinTimer.update();

			LeftFist.transform.RotateAround (Head.transform.position, AORot, spinspeed * Time.deltaTime);
			Vector3 newPos = (LeftFist.transform.position - Head.transform.position).normalized * spinRad + Head.transform.position;
			LeftFist.transform.position = Vector3.MoveTowards (LeftFist.transform.position, newPos, Time.deltaTime * spinspeed * (1 + speed / 100));

			RightFist.transform.RotateAround (Head.transform.position, AORot, spinspeed * Time.deltaTime);
			newPos = (RightFist.transform.position - Head.transform.position).normalized * spinRad + Head.transform.position;
			RightFist.transform.position = Vector3.MoveTowards (RightFist.transform.position, newPos, Time.deltaTime * spinspeed * (1 + speed / 100));

			spinRad += 0.1f;

			if (spinTimer.isReady ()) {
				state = 4;
				restTimer.reset ();
				LeftFist.transform.position = leftfiststartpos;
				RightFist.transform.position = rightfiststartpos;
				spinRad = 2.0f;
			}
		}
	}

	void Die() {
		//Do special things
		Debug.Log("API Boss");
        door1.SetActive(false);
        door2.SetActive(false);
        entrance.SetActive(true);
        GameStateManager.RedShieldBossDead = true;

        //Do default things
        APIBossHealth.Die();
	} 
}

