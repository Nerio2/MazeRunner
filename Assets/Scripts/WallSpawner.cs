using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
	public GameObject Wall;
	public Transform Parrent;

	private void OnTriggerEnter2D(Collider2D collision) {
		Instantiate(Wall, Parrent);
		Destroy(this);
	}
}
