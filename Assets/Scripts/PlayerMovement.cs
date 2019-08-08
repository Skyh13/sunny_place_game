using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    /**
    * Public variables
    *
    *
    */
    // Movement keys
    public KeyCode jumpKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    // Lateral movement variables
	public float moveSpeed;
    public float timeBetweenSteps;

    // Vertical movement variables
    public float jumpSpeed;
    public float fallingGravityModifier;
    public float jumpHoldTime;
    public float jumpHoldModifier;


    /**
    * Private Variables
    * */
    // Vertical movement variables
    float currentJumpHoldTime = 0;
    bool isJumping = false;
    bool isOnGround;

    float currentTimeBetweenSteps = 0;

	Rigidbody2D rb;
    PlayerSound psounds;
	Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        StepSound();
    }

    void FixedUpdate()
    {
        if (isJumping) {
			rb.AddForce(Vector2.up * jumpSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            currentJumpHoldTime = jumpHoldTime;
			isJumping = false;
		}
		else if (!isOnGround && Input.GetKey(jumpKey) && currentJumpHoldTime > 0) {
			rb.AddForce(Vector2.up * jumpSpeed * jumpHoldModifier * currentJumpHoldTime * Time.fixedDeltaTime, ForceMode2D.Impulse);
            currentJumpHoldTime -= Time.fixedDeltaTime;
        }

		if (!isOnGround && rb.velocity.y < 0) {
            // this means we're in the air and falling. i want to apply some additional gravity in this case;
            rb.AddForce(Vector2.down * fallingGravityModifier * Time.fixedDeltaTime, ForceMode2D.Impulse);         
        }

        rb.AddForce(movement * moveSpeed * Time.fixedDeltaTime);
    }

    void StepSound()
    {
        if(isOnGround && Mathf.Abs(rb.velocity.x) > 0.1f) {
            if(currentTimeBetweenSteps > 0) {
                currentTimeBetweenSteps -= Time.deltaTime;
            } else {
                psounds.PlayStepSound();
                currentTimeBetweenSteps = timeBetweenSteps;
            }
        } else {
            currentTimeBetweenSteps = timeBetweenSteps;
        }
    }

    void Move()
    {
        movement = Vector2.zero;

		if (Input.GetKeyDown(jumpKey) && isOnGround) {
			isJumping = true;
            psounds.PlayJumpSound();
		}

		if (Input.GetKeyUp(jumpKey) && !isOnGround) {
			currentJumpHoldTime = 0;
		}

        if (Input.GetKey(downKey)) {

        }

        if (Input.GetKey(leftKey)) {
            movement += Vector2.left;
        }

        if (Input.GetKey(rightKey)) {
            movement += Vector2.right;
        }
    }

    void OnCollisionEnter2D (Collision2D c) {
		if (c.gameObject.tag == "ground") {
			isOnGround = true;
            Vector2 v = rb.velocity;
            v.y = 0;
            rb.velocity = v;
		}
	}

	void OnCollisionExit2D (Collision2D c) {
		if (c.gameObject.tag == "ground") {
			isOnGround = false;
		}	
	}
}
