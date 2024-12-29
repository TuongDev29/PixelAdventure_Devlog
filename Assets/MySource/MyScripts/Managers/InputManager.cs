using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public float MoveInput()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool JumpInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
