using UnityEngine;

public class WallSpawner : MonoBehaviour {
	public GameObject Wall;
	public Transform Parrent;
	private GameObject GStart;

	private void Start() {
		GStart = GameObject.Find("Start");
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Instantiate(Wall , Parrent);
		int i = Wall.transform.name.IndexOf('L') == 0 ? 1 : -1;
		GStart.GetComponent<GameController>().LRChange(i);
		Destroy(this);
	}
}
