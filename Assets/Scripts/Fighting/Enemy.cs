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

        // TODO: implement patrolling logic instead of just return to the original position
        if (toStartingLength < chaseLength)
        {
            Vector3 toPlayer = playerTransform.position - transform.position;

            // start chasing if user is in trigger length and only end when it's out chasing of range
            if (toPlayer.magnitude <= triggerLength && Time.time - lastChase > chaseCooldown)
                chasing = true;

            if (chasing)
            {    
                if (!collidingWithPlayer)
                {
                    // Chasing 
                    UpdateMotor(toPlayer, speed);
                }
            }
            else
            {
                // Still in range but no chasing
                UpdateMotor(toStarting, speed);
            }
        }
        else
        {
            // Out of range
            UpdateMotor(toStarting, speed);
            chasing = false;
            lastChase = Time.time;
        }


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

    protected override void Death()
    {
        GameManager.instance.experience += xpValue;
        GameManager.instance.ShowText(new FloatingText.TextInfoDTO
        {
            Message = $"+{xpValue}xp",
            Color = Color.magenta,
            Motion = Vector3.up * 10,
            Position = transform.position
        });

        Destroy(gameObject, 0.2f);
    }

    /* private void RandomlyPickNewDirection()
    {
        direction = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0).normalized;
    }*/
}
