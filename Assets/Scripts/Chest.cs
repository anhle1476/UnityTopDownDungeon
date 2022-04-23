using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;
    public float collectMsgMotionSpeed = 20f;

    protected override void OnCollect(Collider2D collider)
    {
        GetComponent<SpriteRenderer>().sprite = emptyChest;
        ShowCollectMessage();
    }

    private void ShowCollectMessage()
    {
        GameManager.instance.ShowText(new FloatingText.TextInfoDTO
        {
            Message = $"+{pesosAmount} pesos",
            Position = transform.position,
            Color = Color.yellow,
            Duration = 1f,
            Motion = Vector3.up * collectMsgMotionSpeed
        });
    }
}
