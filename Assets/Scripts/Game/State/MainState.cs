public class MainState : GameState
{
    public MainState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Main Phase";
    }

    public override void StateCall()
    {
        if (IsYourTurn())
            GameController2.Instance.Player.TogglePlayableEffect(true);
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

    public override void EndStateCall()
    {
        if (IsYourTurn())
            GameController2.Instance.Player.TogglePlayableEffect(false);
    }
}