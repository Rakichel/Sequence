namespace PlayerInfo
{
    /// <summary>
    /// �÷��̾��� ������ �����մϴ�.
    /// </summary>
    public enum PlayerDirection { Right, Left }

    /// <summary>
    /// �÷��̾��� ���¸� �����մϴ�.
    /// </summary>
    public enum PlayerState { Idle = 0, Move, Jump, DJump, Landing, Landed, Dash, Attack, Combo, Guard, Guarding, Counter, Hit, Die }
}
