using Assets.Scripts.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int damagePoint = 1;
    public float pushForce = 0.5f;

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
        if (IsFighterHit(collider) && collider.name != "Player")
        {
            var dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            collider.SendMessage(Fighter.RECEIVE_DAMAGE_METHOD_NAME, dmg);
        }
    }
}
