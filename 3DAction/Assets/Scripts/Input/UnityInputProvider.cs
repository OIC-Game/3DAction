using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action3D.Player;

namespace Action3D.InputProvider
{
    /// <summary>
    /// UnityのInputを使用したテスト用入力クラス
    /// </summary>
    public class UnityInputProvider : IInputProvider
    {
        public bool GetForwardMove()
        {
            return Input.GetKey(KeyCode.W);
        }

        public bool GetJump()
        {
            return Input.GetKey(KeyCode.Space);
        }

        public float GetLateralAxis()
        {
            return GetLeftMove() ? -1.0f : GetRightMove() ? 1.0f : 0.0f;
        }

        public bool GetLeftMove()
        {
            return Input.GetKey(KeyCode.A);
        }

        public bool GetRightMove()
        {
            return Input.GetKey(KeyCode.D);
        }

        public bool GetSliding()
        {
            return Input.GetKey(KeyCode.S);
        }

        public bool GetPause()
        {
            return Input.GetKey(KeyCode.R);
        }
    }
}