using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour {
	public GameObject Wall;
	public Transform Parrent;

	private void Start() {
		GameObject Start = GameObject.Find("Start");
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Instantiate(Wall , Parrent);
		if(Wall.transform.name.IndexOf('L')==0)
		Destroy(this);
	}
}
