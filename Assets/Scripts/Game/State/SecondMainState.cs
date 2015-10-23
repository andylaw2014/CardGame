public class SecondMainState : GameState
{
    public SecondMainState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Second Main Phase";
    }

    public override void StateCall()
    {
        if (IsYourTurn())
            GameController.Instance.Player.TogglePlayableEffect(true);
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
        return IsFirstPlayer ? Type.Player2Reset : Type.Player1Reset;
    }

    public override void EndStateCall()
    {
        if (IsYourTurn())
            GameController.Instance.Player.TogglePlayableEffect(false);
    }
}
