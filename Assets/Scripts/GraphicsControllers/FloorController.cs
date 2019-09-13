using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {
	private int numberOfAddons = 5;
	private int chance = 10; // real chance is:  1/chance
	void Start() {
		if ( transform.rotation.z != 0 ) transform.eulerAngles = new Vector3(0 , 0 , 0);
		if ( Random.Range(1 , chance + 1) == 1 ) {
			spawn();
		}
	}

	void spawn() {
		int nr = Random.Range(1 , numberOfAddons + 1);
		Instantiate(Resources.Load<GameObject>("Background/Addon" + nr) , transform);

	}
}
