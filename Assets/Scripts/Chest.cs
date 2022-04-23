using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;
    protected override void OnCollect(Collider2D collider)
    {
        GetComponent<SpriteRenderer>().sprite = emptyChest;
        Debug.Log($"Grant {pesosAmount} pesos to {collider.name}");
    }
}
