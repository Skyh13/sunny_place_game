using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D c) {
        if (c.gameObject.tag == "player" && tag == "changeStageX") {
            CameraMovement cam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            if (transform.position.x < cam.GetIntendedPosition().x) {
                cam.MoveFullLeft(0.5f);
            } else {
                cam.MoveFullRight(0.5f);
            }
        }

        if (c.gameObject.tag == "player" && tag == "changeStageY") {
            CameraMovement cam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            if (transform.position.y < cam.GetIntendedPosition().y) {
                cam.MoveFullDown(0.5f);
            } else {
                cam.MoveFullUp(0.5f);
            }
        }
    }

    void OnTriggerExit2D (Collider2D c) {
        if (c.gameObject.tag == "player" && tag == "changeStageX") {
            CameraMovement cam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            if (c.gameObject.transform.position.x < cam.GetIntendedPosition().x - (cam.GetCameraWidth()/2)) {
                cam.MoveFullLeft(0.5f);
            } 
            
            if (c.gameObject.transform.position.x > cam.GetIntendedPosition().x + (cam.GetCameraWidth()/2)) {
                cam.MoveFullRight(0.5f);
            }
        }

        if (c.gameObject.tag == "player" && tag == "changeStageY") {
            CameraMovement cam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            if (c.gameObject.transform.position.y < cam.GetIntendedPosition().y - (cam.GetCameraHeight()/2)) {
                cam.MoveFullDown(0.5f);
            } 
            
            if (c.gameObject.transform.position.y > cam.GetIntendedPosition().y + (cam.GetCameraHeight()/2)) {
                cam.MoveFullUp(0.5f);
            }
        }
    }
}
