using Assets.Scripts.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int damagePoint = 1;
    public float pushForce = 1f;

    // Upgrade
    public int weaponLevel = 1;
    public SpriteRenderer spriteRenderer;

    // Swing
    private Animator animator;
    public float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("Swing");
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

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel - 1];
    }
}
