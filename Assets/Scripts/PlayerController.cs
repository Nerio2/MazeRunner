﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private float speed;

	private int i = 0;
	public int acceleration = 80; //velocity.xy += deltaTime / acceleration               max acceleration=10, min acceleration=100
	public int startSpeed = 2;

	public Text ScoreText;
	private float score = 0;
	private Vector2 lastPosition;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(0 , startSpeed);
		speed = startSpeed;
		lastPosition = transform.position;
	}

	void Update() {
		//rb.velocity += new Vector2(rb.velocity.x > 0 ? Time.deltaTime / acceleration : rb.velocity.x < 0 ? Time.deltaTime / -acceleration : 0 ,
		//	rb.velocity.y > 0 ? Time.deltaTime / acceleration : rb.velocity.y < 0 ? Time.deltaTime / -acceleration : 0);
		speed += Time.deltaTime / acceleration;

		// Mode 1 left, right 
		/*
		if ( Input.GetKeyDown(KeyCode.LeftArrow) ) {
			i++;


			if ( rb.velocity.x == 0 )
				rb.velocity = new Vector2(rb.velocity.y * -1 , 0);
			else
				rb.velocity = new Vector2(0 , rb.velocity.x);
		} else if ( Input.GetKeyDown(KeyCode.RightArrow) ) {
			i--;


			if ( rb.velocity.x == 0 )
				rb.velocity = new Vector2(rb.velocity.y , 0);
			else
				rb.velocity = new Vector2(0 , rb.velocity.x * -1);
		}
		*/
		//Mode 2, 4 directions
		if ( Input.GetKeyDown(KeyCode.LeftArrow) ) {
			rb.velocity = new Vector2(-speed , 0);
		}
		if ( Input.GetKeyDown(KeyCode.RightArrow) ) {
			rb.velocity = new Vector2(speed , 0);
		}
		if ( Input.GetKeyDown(KeyCode.UpArrow) ) {
			rb.velocity = new Vector2(0 , speed);
		}
		if ( Input.GetKeyDown(KeyCode.DownArrow) ) {
			rb.velocity = new Vector2(0 , -speed);
		}

		score += Vector2.Distance(transform.position , lastPosition);
		lastPosition = transform.position;
		ScoreText.text = "Score: " + (long) score;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if ( collision.name.Contains("Cube") ) {
			Destroy(gameObject);
		}
	}
}