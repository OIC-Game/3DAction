using System.Collections;
using System.Collections.Generic;

namespace Action3D.Player
{
    /// <summary>
    /// 入力用インターフェース
    /// </summary>
    public interface IInputProvider
    {
        bool GetForwardMove();
        bool GetLeftMove();
        bool GetRightMove();

        float GetLateralAxis();

        bool GetSliding();
        bool GetJump();

        bool GetPause();
    }
}