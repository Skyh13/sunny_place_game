using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
/**
    * Public variables
    *
    *
    */

    // Lateral movement variables
	public float moveSpeed;

    public bool ignoresEdges;
    public bool chasesPlayer;


    /**
    * Private Variables
    * */

	Rigidbody2D rb;
    PlayerSound psounds;
	Vector2 movement;

    string moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = "left";
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * moveSpeed * Time.fixedDeltaTime);
    }

    bool DetectPlatformEdge()
    {

        return false;
    }

    void Move()
    {
        if (chasesPlayer) {
            
        }

        if (!ignoresEdges) {
            if (DetectPlatformEdge()) {
                if(rb.velocity.x > 0) {
                    moveDirection = "left";
                } else {
                    moveDirection = "right";
                }
            }
        }

        if (moveDirection == "left") {
            movement = Vector2.left;
        } else if(moveDirection == "right") {
            movement = Vector2.right;
        }
    }

    void OnCollisionEnter2D (Collision2D c) {

	}

	void OnCollisionExit2D (Collision2D c) {

	}

    public void ReverseDirection()
    {
        if (moveDirection == "left") {
            moveDirection = "right";
        } else {
            moveDirection = "left";
        }
    }
}
