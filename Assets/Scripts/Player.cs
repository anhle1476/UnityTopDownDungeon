using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Mover
{
    protected virtual void FixedUpdate()
    {
        float x = Input.GetAxisRaw(Axis.Horizontal.ToString());
        float y = Input.GetAxisRaw(Axis.Vertical.ToString());

        UpdateMotor(new Vector3(x, y, 0));
    }
}
