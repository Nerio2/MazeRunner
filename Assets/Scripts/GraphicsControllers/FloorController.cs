using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

	void Start() {
		if ( transform.rotation.z != 0 ) transform.eulerAngles = new Vector3(0 , 0 , 0);
		GetComponent<SpriteRenderer>().material.color= transform.parent.GetComponent<GameController>().LvlColor;
	}

	void Update() {
		
	}
}
