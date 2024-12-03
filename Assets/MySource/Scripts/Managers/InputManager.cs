using UnityEngine;
using DevLog;

namespace DevLog
{
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
}
