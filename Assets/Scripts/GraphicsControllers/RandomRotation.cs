using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour {
	public int MinRotation = 0;
	public int MaxRotation = 360;

	void Start() {
		transform.eulerAngles = new Vector3(0 , 0 , Random.Range(MinRotation , MaxRotation));
	}
}
