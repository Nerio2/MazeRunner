using UnityEngine;

public class StairsController : MonoBehaviour {

	public GameObject StartObj;
	public GameObject StartPrefab;
	public int MinimumRoadSize = 30;
	public int MaximumRoadSize = 80;

	public float sleep;
	public int rotation;

	private void Start() {
		StartObj = GameObject.FindGameObjectWithTag("GameController");
		StartPrefab = Resources.Load<GameObject>("Objects/Game");
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if ( collision.transform.name.Contains("Player") ) {
			Destroy(StartObj);
			GameObject Obj = Instantiate(StartPrefab , transform.position , transform.rotation , null);
			Obj.GetComponent<GameController>().sleep = sleep;
			Obj.GetComponent<GameController>().rotation = rotation;
			Obj.GetComponent<GameController>().MinimumRoadSize = MinimumRoadSize;
			Obj.GetComponent<GameController>().MaximumRoadSize = MaximumRoadSize;
		}
	}
}
