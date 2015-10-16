public class AttackCounterState : GameState
{
    public AttackCounterState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Attack Counter Phase";
    }

    public override void StateCall()
    {

    }

    public override bool NextPhaseClickable()
    {
        return IsYourTurn();
    }

    public override bool AllowPlayCard()
    {
        return IsYourTurn();
    }

    public override Type NextState()
    {
        return IsFirstPlayer ? Type.Player1DefCot : Type.Player2DefCot;
    }
}
