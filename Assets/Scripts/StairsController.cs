using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour {

	public GameObject StartObj;
	public GameObject StartPrefab;

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
		}
	}
}
