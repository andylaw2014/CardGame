public class DefenceState : GameState
{
    public DefenceState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Defence Phase";
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
        return false;
    }

    public override Type NextState()
    {
        return IsFirstPlayer ? Type.Player1AtkCot : Type.Player2AtkCot;
    }
}
