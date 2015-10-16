public class AttackState : GameState
{
    public AttackState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Attack Phase";
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
        return false;
    }

    public override Type NextState()
    {
        return IsFirstPlayer ? Type.Player1Def : Type.Player2Def;
    }
}
