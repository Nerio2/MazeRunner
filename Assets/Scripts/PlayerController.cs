﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private int i = 0;

	public Camera camera;
	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(0 , 1);
	}

	// Update is called once per frame
	void Update() {
		if ( Input.GetKeyDown(KeyCode.LeftArrow) ) {
			i++;
			transform.eulerAngles = Vector3.forward * ( i * 90f );


			if ( rb.velocity.x == 0 )
				rb.velocity = new Vector2(rb.velocity.y * -1 , 0);
			else
				rb.velocity = new Vector2(0 , rb.velocity.x);
		} else if ( Input.GetKeyDown(KeyCode.RightArrow) ) {
			i--;
			transform.eulerAngles = Vector3.forward * ( i * 90f );


			if ( rb.velocity.x == 0 )
				rb.velocity = new Vector2(rb.velocity.y , 0);
			else
				rb.velocity = new Vector2(0 , rb.velocity.x * -1);
		}
	}
}