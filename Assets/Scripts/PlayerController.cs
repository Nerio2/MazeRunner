using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private float speed;
	private int direction = 0; //0-north 1-west 2-south 3-east

	private int i = 0;
	public int acceleration = 60; //velocity.xy += deltaTime / acceleration               max acceleration=10, min acceleration=100
	public float startSpeed = 1.4f;

	public Text ScoreText;
	private int score = 0;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(0 , startSpeed);
		speed = startSpeed;
	}

	void Update() {
		speed += Time.deltaTime / acceleration;
		switch ( direction ) {
			case 0:
				rb.velocity = new Vector2(0 , speed);
				break;
			case 1:
				rb.velocity = new Vector2(-speed , 0);
				break;
			case 2:
				rb.velocity = new Vector2(0 , -speed);
				break;
			case 3:
				rb.velocity = new Vector2(speed , 0);
				break;
			default:
				Debug.Log("Player direction error");
				break;
		}
		if ( Input.anyKeyDown ) {
			if ( Input.GetKeyDown(KeyCode.UpArrow) ) {
				direction = 0;
			} else if ( Input.GetKeyDown(KeyCode.LeftArrow) ) {
				direction = 1;
			} else if ( Input.GetKeyDown(KeyCode.DownArrow) ) {
				direction = 2;
			} else if ( Input.GetKeyDown(KeyCode.RightArrow) ) {
				direction = 3;
			}
		}

		ScoreText.text = "Score: " + score;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if ( collision.name.Contains("Cube") ) {
			Destroy(gameObject);
		} else if ( collision.GetComponent<RoadElement>().pkt ) {
			score++;
			collision.GetComponent<RoadElement>().pkt = false;
		}
	}
}