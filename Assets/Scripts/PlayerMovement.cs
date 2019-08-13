using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int maxHealth;


    /**
    * Private Variables
    * */
    // Vertical movement variables
    float currentJumpHoldTime = 0;
    bool isJumping = false;
    bool isOnGround;

    float currentTimeBetweenSteps = 0;

    int currentHealth;

	Rigidbody2D rb;
    PlayerSound psounds;
	Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        psounds = GetComponent<PlayerSound>();
        currentHealth = maxHealth;
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
        else if (c.gameObject.tag == "enemy")
        {
            // Player can still jump off the enemy's head, but loses 1 health
            currentHealth--;
            UIHealthBar.instance.SetValue((float)currentHealth / maxHealth);

            if (currentHealth == 0)
            {
                SceneManager.LoadScene(3);
            }
            isOnGround = true;
            //Vector2 v = rb.velocity;
            //v.y = 0;
            //rb.velocity = v;
        }
    }

	void OnCollisionExit2D (Collision2D c) {
		if (c.gameObject.tag == "ground") {
			isOnGround = false;
		}	
	}

    void OnTriggerEnter2D (Collider2D c) {
        if (c.gameObject.tag == "changeStageX") {
            CameraMovement cam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            if (transform.position.x < cam.transform.position.x) {
                cam.MoveFullLeft(0.5f);
            } else {
                cam.MoveFullRight(0.5f);
            }
        }

        if (c.gameObject.tag == "changeStageY") {
            CameraMovement cam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            if (transform.position.y < cam.transform.position.y) {
                cam.MoveFullDown(0.5f);
            } else {
                cam.MoveFullUp(0.5f);
            }
        }
    }

    void OnTriggerExit2D (Collider2D c) {
        if (c.gameObject.tag == "changeStageX") {
            CameraMovement cam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            if (transform.position.x < cam.transform.position.x - (cam.GetCameraWidth()/2)) {
                cam.MoveFullLeft(0.5f);
            } 
            
            if (transform.position.x > cam.transform.position.x + (cam.GetCameraWidth()/2)) {
                cam.MoveFullRight(0.5f);
            }
        }

        if (c.gameObject.tag == "changeStageY") {
            CameraMovement cam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            if (transform.position.y < cam.transform.position.y - (cam.GetCameraHeight()/2)) {
                cam.MoveFullDown(0.5f);
            } 
            
            if (transform.position.y > cam.transform.position.y + (cam.GetCameraHeight()/2)) {
                cam.MoveFullUp(0.5f);
            }
        }
    }
}
