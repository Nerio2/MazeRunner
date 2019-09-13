using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	private GameObject Player;
	public GameObject TurnLeft;
	public GameObject TurnRight;
	public GameObject Wall;
	public GameObject TurnLR;
	public GameObject Stairs;

	public GameObject Floor;

	private int length = 0;
	private int maxLength;     //random betweeen 
	public int MinimumRoadSize = 30;
	public int MaximumRoadSize = 80;

	private List<Vector3> positions = new List<Vector3>();
	private int acceleration;
	private int numerOfElements = 3;
	private Transform tr;
	[HideInInspector]
	public int rotation = 0;  //0-north 1-west 2-south 3-east
	private Vector3 nextPos;
	public float sleep = 1f;
	private float time = 30f;
	private bool [] overlaps = { false , false , false , false };

	public Material LvlColor;

	// Start is called before the first frame update
	void Start() {

		LvlColor.color = Random.ColorHSV();

		maxLength = Random.Range(MinimumRoadSize , MaximumRoadSize);
		Player = GameObject.Find("Player");
		tr = Player.transform;
		nextPos = transform.position;
		positions.Add(nextPos);
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
		positions.Add(nextPos);
		next();
		acceleration = Player.GetComponent<PlayerController>().acceleration;
	}

	// Update is called once per frame
	void Update() {
		sleep -= Time.deltaTime / ( 5 * acceleration );
		if ( rotation > 3 )
			rotation = 0;
		if ( rotation < 0 )
			rotation = 3;
		time += Time.deltaTime;
		if ( time > sleep ) {
			next();
			time -= sleep;
		}
	}

	//generating next part of maze
	void next() {
		int rand;
		do {
			if ( isBlocked() ) {
				rand = 99;
				break;
			}
			rand = Random.Range(0 , numerOfElements);

		} while ( overlaps [rand]);
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
				break;
			case 99:
				restart();
				break;
			default:
				Debug.Log("Error");
				break;
		}
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
		length++;
		Instantiate(obj , nextPos , Quaternion.Euler(new Vector3(0 , 0 , rotation * 90)) , transform);
		Instantiate(Floor , nextPos , Quaternion.Euler(new Vector3(0 , 0 , 0)) , transform);
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
	}

	private bool isBlocked() {
		if ( length >= maxLength )
			return true;
		else {
			for ( int i = 0 ; i < 4 ; i++ )
				if ( overlaps [i] == false )
					return false;
			return true;
		}
	}

	private void restart() {
		GameObject Obj = Instantiate(Stairs , nextPos , Quaternion.Euler(new Vector3(0 , 0 , rotation * 90)) , transform);
		Obj.GetComponent<StairsController>().sleep = sleep;
		Obj.GetComponent<StairsController>().rotation = rotation;
		Obj.GetComponent<StairsController>().MinimumRoadSize = MinimumRoadSize;
		Obj.GetComponent<StairsController>().MaximumRoadSize = MaximumRoadSize;
		Destroy(this);
	}
}
