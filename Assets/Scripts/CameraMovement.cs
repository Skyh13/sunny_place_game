using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Camera cam;

    Vector3 intendedPosition;

    void Start()
    {
        cam = GetComponent<Camera>();
        intendedPosition = cam.transform.position;
    }

    public void MoveFullLeft(float seconds = 0)
    {
        intendedPosition = new Vector3(intendedPosition.x - (2f * cam.orthographicSize * cam.aspect), intendedPosition.y, intendedPosition.z);
        Move(cam.transform.position, intendedPosition, seconds);
    }

    public void MoveFullRight(float seconds = 0)
    {
        intendedPosition = new Vector3(intendedPosition.x + (2f * cam.orthographicSize * cam.aspect), intendedPosition.y, intendedPosition.z);
        Move(cam.transform.position, intendedPosition, seconds);
    }

    public void MoveFullUp(float seconds = 0)
    {
        intendedPosition = new Vector3(intendedPosition.x, intendedPosition.y + (2f * cam.orthographicSize), intendedPosition.z);
        Move(cam.transform.position, intendedPosition, seconds);
    }

    public void MoveFullDown(float seconds = 0)
    {
        intendedPosition = new Vector3(intendedPosition.x, intendedPosition.y - (2f * cam.orthographicSize), intendedPosition.z);
        Move(cam.transform.position, intendedPosition, seconds);
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

    public Vector3 GetIntendedPosition()
    {
        return intendedPosition;
    }

    IEnumerator SmoothMove (Vector3 startpos, Vector3 endpos, float seconds) {
        float t = 0.0f;
        while (t <= 1.0) {
            t += Time.deltaTime/seconds;
            cam.transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }
}
