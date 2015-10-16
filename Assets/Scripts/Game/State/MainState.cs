public class MainState : GameState
{
    public MainState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Main Phase";
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
        return IsFirstPlayer ? Type.Player1Atk : Type.Player2Atk;
    }
}
