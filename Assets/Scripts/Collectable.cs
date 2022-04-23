using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    protected bool isCollected;

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name == "Player" && !isCollected)
        {
            OnCollect(collider);
            isCollected = true;
        }
    }

    protected virtual void OnCollect(Collider2D collider)
    {

    }
}
