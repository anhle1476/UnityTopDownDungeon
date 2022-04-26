using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Mover
{
    public float speed = 1.0f;

    protected virtual void FixedUpdate()
    {
        float x = Input.GetAxisRaw(Axis.Horizontal.ToString());
        float y = Input.GetAxisRaw(Axis.Vertical.ToString());

        UpdateMotor(new Vector3(x, y, 0), speed, stopOnClose: false);
    }

    protected override void Death()
    {
        GameManager.instance.ShowText(new FloatingText.TextInfoDTO
        {
            Message = "GAME OVER",
            Color = Color.white,
            FontSize = 50,
            Position = Vector3.zero,
            Duration = 1000,
        });
    }
}
