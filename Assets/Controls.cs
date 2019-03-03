using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls
{
    public enum KeysetName
    {
        Arrows,
        WASD
    }

    public static Keyset GetKeyset(KeysetName kn)
    {
        Keyset ks = new Keyset();
        if(kn.Equals(KeysetName.Arrows))
        {
            ks.LEFT = KeyCode.LeftArrow;
            ks.RIGHT = KeyCode.RightArrow;
            ks.UP = KeyCode.UpArrow;
            ks.DOWN = KeyCode.DownArrow;
            ks.JUMP = KeyCode.Space;
            ks.ATTACK = KeyCode.M;
        }

        else if (kn.Equals(KeysetName.WASD))
        {
            ks.LEFT = KeyCode.A;
            ks.RIGHT = KeyCode.D;
            ks.UP = KeyCode.W;
            ks.DOWN = KeyCode.S;
            ks.JUMP = KeyCode.R;
            ks.ATTACK = KeyCode.E;
        }
        return ks;
    }
}
