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
        StateChange();
    }

    private void StateChange()
    {

    }
}
