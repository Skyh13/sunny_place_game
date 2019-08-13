using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void MoveFullLeft(float seconds = 0)
    {
        Move(cam.transform.position, new Vector3(cam.transform.position.x - (2f * cam.orthographicSize * cam.aspect), cam.transform.position.y, cam.transform.position.z), seconds);
    }

    public void MoveFullRight(float seconds = 0)
    {
        Move(cam.transform.position, new Vector3(cam.transform.position.x + (2f * cam.orthographicSize * cam.aspect), cam.transform.position.y, cam.transform.position.z), seconds);
    }

    public void MoveFullUp(float seconds = 0)
    {
        Move(cam.transform.position, new Vector3(cam.transform.position.x, cam.transform.position.y + (2f * cam.orthographicSize), cam.transform.position.z), seconds);
    }

    public void MoveFullDown(float seconds = 0)
    {
        Move(cam.transform.position, new Vector3(cam.transform.position.x, cam.transform.position.y - (2f * cam.orthographicSize), cam.transform.position.z), seconds);
    }

    public void Move(Vector3 start, Vector3 end, float seconds = 1f)
    {
        StartCoroutine(SmoothMove(start, end, seconds));
    }

    public Camera GetCamera()
    {
        return cam;
    }

    public float GetCameraHeight()
    {
        return 2f * cam.orthographicSize;
    }

    public float GetCameraWidth()
    {
        return GetCameraHeight() * cam.aspect;
    }

    IEnumerator SmoothMove (Vector3 startpos, Vector3 endpos, float seconds) {
        float t = 0.0f;
        while (t <= 1.0) {
            t += Time.deltaTime/seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }
}
