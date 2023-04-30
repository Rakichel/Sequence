using BossInfo;

public class BossIdle : IBossTodo
{
    BossController _controller;
    public BossIdle(BossController _controller)
    {
        this._controller = _controller;
    }

    public void Work()
    {
        if (_controller.Player != null)
        {
            PlayerChase();
        }
    }

    private void PlayerChase()
    {
        _controller.State = BossState.Move;
    }
}
