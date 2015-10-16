public class DefenceCounterState : GameState
{
    public DefenceCounterState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Defence Counter Phase";
    }

    public override void StateCall()
    {

    }

    public override bool NextPhaseClickable()
    {
        return !IsYourTurn();
    }

    public override bool AllowPlayCard()
    {
        return !IsYourTurn();
    }

    public override Type NextState()
    {
        return IsFirstPlayer ? Type.Player1SecMain : Type.Player2SecMain;
    }
}
