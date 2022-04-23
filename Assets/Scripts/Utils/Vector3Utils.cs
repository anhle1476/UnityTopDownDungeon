using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Vector3Utils
    {
        public static Vector3 Vertical(float y)
        {
            return y switch
            {
                0 => Vector3.zero,
                1 => Vector3.up,
                -1 => Vector3.down,
                _ => new Vector3(0, y, 0),
            };
        }

        public static Vector3 Horizontal(float x)
        {
            return x switch
            {
                0 => Vector3.zero,
                1 => Vector3.right,
                -1 => Vector3.left,
                _ => new Vector3(0, x, 0),
            };
        }

        public static Vector3 Up(float amount)
        {
            return Vertical(amount);
        }

        public static Vector3 Down(float amount)
        {
            return Vertical(-amount);
        }

        public static Vector3 Right(float amount)
        {
            return Horizontal(amount);
        }

        public static Vector3 Left(float amount)
        {
            return Horizontal(-amount);
        }

    }
}
