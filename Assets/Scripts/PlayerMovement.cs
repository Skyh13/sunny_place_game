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

    // Vertical movement variables
    public float jumpSpeed;
    public float fallingGravityModifier;
    public float jumpHoldTime;
    public float jumpHoldModifier;


    /**
    * Private Variables
    * */
    // Vertical movement variables
    float currentJumpHoldTime;
    bool isJumping;
    bool isOnGround;

	Rigidbody2D rb;
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
    }

    void FixedUpdate()
    {
        if (isJumping) {
			rb.AddForce(Vector2.up * jumpSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
			isJumping = false;
		}
		else if (!isOnGround && Input.GetKey(jumpKey) && currentJumpHoldTime > 0) {
			rb.AddForce(Vector2.up * jumpSpeed * jumpHoldModifier * currentJumpHoldTime * Time.fixedDeltaTime, ForceMode2D.Impulse);
		}

		if (!isOnGround && rb.velocity.y < 0) {
			// this means we're in the air and falling. i want to apply some additional gravity in this case;
            rb.AddForce(Vector2.down * fallingGravityModifier * Time.fixedDeltaTime, ForceMode2D.Impulse);
			//Vector2 v = rb.velocity;
			//v.y -= fallingGravityModifier * Time.fixedDeltaTime;
			//rb.velocity = v;			
		}

        rb.AddForce(movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Move()
    {
        movement = Vector2.zero;

		if (Input.GetKeyDown(jumpKey) && isOnGround) {
			isJumping = true;
            //jumpSound.Play();
		}

		if (Input.GetKeyUp(jumpKey) && !isOnGround) {
			currentJumpHoldTime = 0;
		}

        if (Input.GetKey(jumpKey) && !isOnGround) {
			currentJumpHoldTime -= Time.deltaTime;
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
			currentJumpHoldTime = jumpHoldTime;
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
