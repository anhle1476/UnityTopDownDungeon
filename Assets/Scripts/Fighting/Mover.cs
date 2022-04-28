using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Mover : Fighter
{
    public List<string> movementBlockingLayers = new List<string> { "Actor", "Blocking" };

    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Move the object positon to the target direction with a specified speed
    /// </summary>
    /// <param name="toTargetPosition"></param>
    /// <param name="movementSpeed">Movement speed of the object</param>
    /// <param name="stopOnClose">try to stop when come close to the target position</param>
    protected virtual void UpdateMotor(Vector3 toTargetPosition, float movementSpeed = 1, bool stopOnClose = true)
    {
        if (IsDeath) 
            return;

        var direction = toTargetPosition.normalized;
        // update MoveDelta
        moveDelta = new Vector3(direction.x * xSpeed * movementSpeed, direction.y * ySpeed * movementSpeed, 0);

        if (stopOnClose && toTargetPosition.magnitude <= moveDelta.magnitude)
        {
            // Distance to the target is smaller then the move delta, we will move to the target directly instead of go pass it
            moveDelta = toTargetPosition;
        }

        // flip the character facing direction left/right
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        moveDelta += pushDirection;

        // gradually reduce the push vector to zero
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        int blockingLayerMask = LayerMask.GetMask(movementBlockingLayers.ToArray()); ;

        // hit for y direction
        hit = Physics2D.BoxCast(
            origin: transform.position,
            size: boxCollider.size,
            angle: 0,
            direction: new Vector2(0, moveDelta.y),
            distance: Mathf.Abs(moveDelta.y * Time.deltaTime),
            layerMask: blockingLayerMask);


        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        else
        {
            SpecificMovingOnYStuck(moveDelta, hit.collider);
        }

        // hit for x direction
        hit = Physics2D.BoxCast(
            origin: transform.position,
            size: boxCollider.size,
            angle: 0,
            direction: new Vector2(moveDelta.x, 0),
            distance: Mathf.Abs(moveDelta.x * Time.deltaTime),
            layerMask: blockingLayerMask);

        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
        else
        {
            SpecificMovingOnXStuck(moveDelta, hit.collider);
        }
    }

    /// <summary>
    /// Specific handling the movement when Y is stuck on moving. 
    /// Otherwise, there will be no Y direction movement by default.
    /// </summary>
    /// <param name="moveDelta"></param>
    /// <param name="collider"></param>
    protected virtual void SpecificMovingOnYStuck(Vector3 moveDelta, Collider2D collider)
    {

    }

    /// <summary>
    /// Specific handling the movement when Y is stuck on moving. 
    /// Otherwise, there will be no X direction movement by default.
    /// </summary>
    /// <param name="moveDelta"></param>
    /// <param name="collider"></param>
    protected virtual void SpecificMovingOnXStuck(Vector3 moveDelta, Collider2D collider)
    {

    }
}
