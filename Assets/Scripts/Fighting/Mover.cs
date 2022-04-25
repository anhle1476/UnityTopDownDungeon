using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        // update MoveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        // flip the character facing direction left/right
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // hit for y direction
        hit = Physics2D.BoxCast(
            origin: transform.position,
            size: boxCollider.size,
            angle: 0,
            direction: new Vector2(0, moveDelta.y),
            distance: Mathf.Abs(moveDelta.y * Time.deltaTime),
            layerMask: LayerMask.GetMask("Actor", "Blocking"));


        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        // hit for x direction
        hit = Physics2D.BoxCast(
            origin: transform.position,
            size: boxCollider.size,
            angle: 0,
            direction: new Vector2(moveDelta.x, 0),
            distance: Mathf.Abs(moveDelta.x * Time.deltaTime),
            layerMask: LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
