using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float movementSpeed;

    public float distanceFromCenter;

    public enum MovementAxis
    {
        Y_AXIS, X_AXIS
    }

    public MovementAxis movementAxis;
    float direction = 1;

    Vector2 startingPosition;
    Vector2 velocity;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if(movementAxis == MovementAxis.Y_AXIS) {
            if (direction > 0) {
                velocity = Vector2.Lerp(velocity, Vector2.up, movementSpeed);
            } else {
                velocity = Vector2.Lerp(velocity, Vector2.down, movementSpeed);
            }
            if (transform.position.y > (startingPosition.y + distanceFromCenter)) {
                    direction = -1;
            } else if (transform.position.y < (startingPosition.y - distanceFromCenter)) {
                    direction = 1;
            }
        }

        if(movementAxis == MovementAxis.X_AXIS) {
            if (direction > 0) {
                velocity = Vector2.Lerp(velocity, Vector2.right, movementSpeed);
            } else {
                velocity = Vector2.Lerp(velocity, Vector2.left, movementSpeed);
            }
            if (transform.position.x > (startingPosition.x + distanceFromCenter)) {
                    direction = -1;
            } else if (transform.position.x < (startingPosition.x - distanceFromCenter)) {
                    direction = 1;
            }
        }

        float newPosX = rb.position.x + velocity.x * movementSpeed * Time.deltaTime;
        float newPosY = rb.position.y + velocity.y * movementSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2(newPosX, newPosY);
        rb.MovePosition(newPos);
    }

}
