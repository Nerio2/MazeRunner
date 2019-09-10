using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject Player;
	private Vector3 offset;

	private void Start() {
		offset = transform.position - Player.transform.position;
	}

	private void LateUpdate() {
		if ( Player != null )
			transform.position = Player.transform.position + offset;
		else
			GetComponent<Camera>().orthographicSize += 0.05f;
	}
}
