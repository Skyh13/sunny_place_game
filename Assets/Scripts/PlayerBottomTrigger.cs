using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottomTrigger : MonoBehaviour
{

    PlayerMovement playerMovement;
    Rigidbody2D rb;

    bool isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        rb = GetComponentInParent<Rigidbody2D>();
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D c) {
        isTriggered = true;

        if (c.gameObject.tag == "ground" || c.gameObject.tag == "enemy") {
			playerMovement.SetOnGround(true);
            Vector2 v = rb.velocity;
            v.y = 0;
            rb.velocity = v;
            
		}
    }

    void OnTriggerExit2D (Collider2D c) {
        /*
        if (c.gameObject.tag == "changeStageX") {
            CameraMovement cam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            if (transform.position.x < cam.GetIntendedPosition().x - (cam.GetCameraWidth()/2)) {
                cam.MoveFullLeft(0.5f);
            } 
            
            if (transform.position.x > cam.GetIntendedPosition().x + (cam.GetCameraWidth()/2)) {
                cam.MoveFullRight(0.5f);
            }
        }

        if (c.gameObject.tag == "changeStageY") {
            CameraMovement cam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            if (transform.position.y < cam.GetIntendedPosition().y - (cam.GetCameraHeight()/2)) {
                cam.MoveFullDown(0.5f);
            } 
            
            if (transform.position.y > cam.GetIntendedPosition().y + (cam.GetCameraHeight()/2)) {
                cam.MoveFullUp(0.5f);
            }
        }
        */

        isTriggered = false;

        if (c.gameObject.tag == "ground" || c.gameObject.tag == "enemy") {
			playerMovement.SetOnGround(false);
		}	
    }

    public bool GetIsTriggered()
    {
        return isTriggered;
    }
}
