using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Public fields
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;

    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    // Push
    protected Vector3 pushDirection;

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            ShowDamageReceived(dmg.damageAmount);
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;


            if (hitPoint <= 0) 
            {
                hitPoint = 0;
                Death();
            }
        }

    }

    protected virtual void Death()
    {

    }

    private void ShowDamageReceived(int damageAmount)
    {
        GameManager.instance.ShowText(new FloatingText.TextInfoDTO
        {
            Color = Color.red,
            Message = "-" + damageAmount,
            Motion = Vector3.up * 20,
            Position = transform.position
        });
    }
}
