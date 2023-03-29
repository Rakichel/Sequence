namespace PlayerInfo
{
    /// <summary>
    /// 플레이어의 방향을 열거합니다.
    /// </summary>
    public enum PlayerDirection { Right, Left }

    /// <summary>
    /// 플레이어의 상태를 열거합니다.
    /// </summary>
    public enum PlayerState { Idle = 0, Move, Jump, Dash, Attack, Hit, Die }
}
