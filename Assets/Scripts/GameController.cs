using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject Player;
	public GameObject TurnLeft;
	public GameObject TurnRight;
	public GameObject Wall;
	public GameObject TurnLR;

	private List<Vector3> positions = new List<Vector3>();

	private Transform tr;
	public int rotation = 0;  //0-north 1-west 2-south 3-east
	public Vector3 nextPos;
	private float sleep = 2f;
	private float time = 0f;
	public bool [] overlaps = { false , false , false, false };
	public bool block;

	// Start is called before the first frame update
	void Start() {
		tr = Player.transform;
		positions.Add(new Vector3(0 , 0 , 0));
		nextPos = transform.position + new Vector3(0 , 2 , 0);
		next();
	}

	// Update is called once per frame
	void Update() {
		if ( rotation > 3 )
			rotation = 0;
		if ( rotation < 0 )
			rotation = 3;
		time += Time.deltaTime;
		if ( time > sleep ) {
			if(!block)
				next();
			time = 0;
		}
	}

	//generating next part of maze
	void next() {
		int rand;
		do {
			rand = Random.Range(0 , 4);
		} while ( overlaps [rand] );
				for ( int i = 0 ; i < 4 ; i++ ) 
			overlaps [i] = false;

		switch ( rand ) {
			case 0:
				create(TurnRight);
				rotation--;
				break;
			case 1:
				create(Wall);
				break;
			case 2:
				create(TurnLeft);
				rotation++;
				break;
			case 3:
				create(TurnLR);
				block = true;
				break;
			default:
				Debug.Log("Error");
				break;
		}
		if ( !block ) {
			if ( rotation < 0 )
				rotation = 3;
			if ( rotation > 3 )
				rotation = 0;
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
			check(rotation);
		}
	}

	void check(int rotation) {
		for ( int i = -1 ; i < 2 ; i++ ) {
			int r = rotation;
			if ( r + i > 3 )
				r = -1;
			if ( r + i < 0 )
				r = 4;
			overlaps [i + 1] = busy(r + i);
		}
		overlaps [3] = overlaps [0] || overlaps [2];
	}
	bool busy(int rotation) {
		Vector3 colPos = nextPos;
		switch ( rotation ) {
			case 0:
				colPos += new Vector3(0 , 2 , 0);
				break;
			case 1:
				colPos += new Vector3(-2 , 0 , 0);
				break;
			case 2:
				colPos += new Vector3(0 , -2 , 0);
				break;
			case 3:
				colPos += new Vector3(2 , 0 , 0);
				break;
		}
		bool skip = false;
		positions.ForEach(x => {
			if ( x.Equals(colPos) ) {
				skip = true;
				return;
			}
		});
		return skip;
	}
	void create(GameObject obj) {
		Instantiate(obj , nextPos , Quaternion.Euler(new Vector3(0 , 0 , rotation * 90)) , null);
		positions.Add(nextPos);
	}
	public void LRChange(int rot) {
		rotation += rot;
		if ( rotation < 0 )
			rotation = 3;
		if ( rotation > 3 )
			rotation = 0;
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
		check(rotation);
		block = false;
	}
}
