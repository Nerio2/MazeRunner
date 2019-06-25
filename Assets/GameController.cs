using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject Player;
	public GameObject TurnLeft;
	public GameObject TurnRight;
	public GameObject Wall;

	private Transform tr;
	private int rotation = 0;  //0-north 1-west 2-south 3-east
	private Vector3 nextPos;
	private float sleep = 2f;
	private float time = 0f;

	// Start is called before the first frame update
	void Start() {
		tr = Player.transform;
		nextPos = transform.position + new Vector3(0 , 2 , 0);
		next();
	}

	// Update is called once per frame
	void Update() {
		if ( rotation > 3 )
			rotation = 0;
		time += Time.deltaTime;
		if ( time > sleep ) {
			next();
			time = 0;
		}
	}

	//generating next part of maze
	void next() {
		int rand = Random.Range(0 , 3);
		switch ( rand ) {
			case 0:
				create(Wall);
				break;
			case 1:
				create(TurnLeft);
				rotation++;
				break;
			case 2:
				create(TurnRight);
				rotation--;
				break;
			default:
				Debug.Log("Error");
				break;
		}
		switch ( rotation ) {
			case 0:
				nextPos += new Vector3(0 , 2 , 0);
				break;
			case 1:
				nextPos += new Vector3(-2 , 0 , 0);
				break;
			case 2:
				nextPos += new Vector3(0 , -2 , 0);
				break;
			case 3:
				nextPos += new Vector3(2 , 0 , 0);
				break;
		}
	}
	void create(GameObject obj) {
		Instantiate(obj , nextPos , new Quaternion(0 , 0 , rotation * 90 , 0) , null);
	}
}
