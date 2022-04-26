using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serve as the "weapon" for the enemy, players will be damage when they are inside the hitbox
/// </summary>
public class EnemyHitbox : Collidable
{
    public int damagePoint = 1;
    public float pushForce = 1f;
    
    public float hitCooldown = 0.5f;
    private float lastHit;

    protected override void OnCollide(Collider2D collider)
    {
        if (IsPlayerHit(collider) && Time.time - lastHit > hitCooldown)
        {
            var dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            collider.SendMessage(Fighter.RECEIVE_DAMAGE_METHOD_NAME, dmg);
            lastHit = Time.time;
        }
    }
}
