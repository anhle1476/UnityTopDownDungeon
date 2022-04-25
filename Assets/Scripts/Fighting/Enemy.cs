using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Experience
    public int xpValue = 1;

    // Logic
    public float speed = 0.5f;
    public float triggerLength = 0.1f;
    public float chaseLength = 0.5f;

    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //private Vector3 direction;

    // Hitbox
    public ContactFilter2D contactFilter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    private float lastChase;
    private float chaseCooldown = 0.2f;

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        // hit box object is a child of the main object. Otherwise BoxCollider2D will be the same as the "weapon can hit box"
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    protected virtual void FixedUpdate()
    {
        Vector3 toStarting = startingPosition - transform.position;
        float toStartingLength = toStarting.magnitude;
        Debug.Log("To Starting length " + toStartingLength);

        bool shouldChase = false;
        if (toStartingLength < chaseLength)
        {
            Vector3 toPlayer = playerTransform.position - transform.position;
            shouldChase = toPlayer.magnitude <= triggerLength && Time.time - lastChase > chaseCooldown;

            if (shouldChase)
            {
                if (!collidingWithPlayer)
                {
                    Debug.Log("Chasing ");
                    UpdateMotor(toPlayer.normalized * speed);
                }
            }
            else
            {
                Debug.Log("Out of trigger ");
                ToStartingPoint(toStarting);
            }
        }
        else
        {
            Debug.Log("Out of range ");
            ToStartingPoint(toStarting);
            shouldChase = false;
        }

        if (!shouldChase && chasing)
        {
            lastChase = Time.time;
        }
        chasing = shouldChase;

        collidingWithPlayer = false;
        boxCollider.OverlapCollider(contactFilter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            // free the collider array
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D collider)
    {
        if (collider.CompareTag(Tags.FIGHTER) && collider.name == "Player")
        {
            collidingWithPlayer = true;
        }
    }

    private void ToStartingPoint(Vector3 toStarting)
    {
        // not update if the distance to starting point is <=1 as UpdateMotor will apply the X and Y speed
        // -> the movement distance might not be correct and we will not be able to return to it forever
        if (toStarting.magnitude > 0.05)
            UpdateMotor(toStarting.normalized * speed);
    }

    /* private void RandomlyPickNewDirection()
    {
        direction = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0).normalized;
    }*/
}
