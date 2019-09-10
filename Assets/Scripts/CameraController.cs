using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {
	public GameObject Player;
	private Vector3 offset;
	private float timeToEndScene = 2f;
	private float time = 0f;

	private void Start() {
		offset = transform.position - Player.transform.position;
	}

	private void LateUpdate() {
		if ( Player != null )
			transform.position = Player.transform.position + offset;
		else {
			if ( time > timeToEndScene ) {
				SceneManager.LoadScene("EndScene" , LoadSceneMode.Single);
			} else {
				GetComponent<Camera>().orthographicSize += 0.03f;
				time += Time.deltaTime;
			}


		}
	}
}
