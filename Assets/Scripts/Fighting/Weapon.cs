using Assets.Scripts.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    // Upgrade
    public int weaponLevel = 1;
    public SpriteRenderer spriteRenderer;

    // Swing
    public float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    protected override void SpecificUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    private void Swing()
    {
        Debug.Log("Swing");
        //transform.Rotate(0, 0, -5f, Space.Self);
    }

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.CompareTag(Tags.FIGHTER) && collider.name != "Player")
        {
            Debug.Log("Hit " + collider.name);

            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            collider.SendMessage("ReceiveDamage", dmg);
        }
    }
}
