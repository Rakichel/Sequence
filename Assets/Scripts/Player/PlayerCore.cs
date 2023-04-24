namespace PlayerInfo
{
    /// <summary>
    /// �÷��̾��� ������ �����մϴ�.
    /// </summary>
    public enum PlayerDirection { Right = 0, Left }

    /// <summary>
    /// �÷��̾��� ���¸� �����մϴ�.
    /// </summary>
    public enum PlayerState { Idle = 0, Move, Jump, DJump, Landing, Landed, Dash, Attack, Combo, Counter, Guard, Guarding, Hit, Die }
}
