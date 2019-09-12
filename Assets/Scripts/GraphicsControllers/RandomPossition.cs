using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPossition : MonoBehaviour {
	public int Range = 5;
	void Start() {
		Vector2 OldPosition = transform.position;
		transform.position = new Vector2(Random.Range(-Range , Range) / 10f , Random.Range(-Range , Range) / 10f)+OldPosition;
	}
}
