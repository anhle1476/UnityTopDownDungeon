using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.3f;
    public float boundY = 0.15f;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        float deltaY = lookAt.position.y - transform.position.y;

        // character move outside of the camera bounding in X axis
        if (deltaX < -boundX || deltaX > boundX)
        {
            if (deltaX < 0) 
            {
                // move left. Ex: deltaX = -2 | bounding = 1  -> X need to move -1
                delta.x += deltaX + boundX;
            }
            else
            {
                // move right. Ex: deltaX = 2 | bounding = 1  -> X need to move 1
                delta.x += deltaX - boundX;
            }
        }

        // character move outside of the camera bounding in Y axis
        if (deltaY < -boundY || deltaY > boundY)
        {
            if (deltaY < 0)
            {
                // move down. Ex: deltaY = -2 | bounding = 1  -> Y need to move -1
                delta.y += deltaY + boundY;
            }
            else
            {
                // move up. Ex: deltaY = 2 | bounding = 1  -> X need to move 1
                delta.y += deltaY - boundY;
            }
        }

        // append the camera delta to it current position
        transform.position += delta;
    }
}
