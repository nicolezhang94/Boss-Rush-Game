using UnityEngine;
using UnityEngine.SceneManagement;
using BossRush.Common;

public class ZoneTrigger : MonoBehaviour {

    public string Scene;

	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameStateManager.CurrentScene = Scene;
            GameStateManager.PreviousScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(Scene);
        }
    }
}
