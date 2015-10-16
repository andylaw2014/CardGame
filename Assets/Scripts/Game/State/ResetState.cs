public class ResetState : GameState
{
    public ResetState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Reset Phase";
    }

    public override void StateCall()
    {
        if(IsYourTurn()&& !GameController.Instance.Player.IsResourceAllFull())
            GameController.Instance.ResourcePanel.Activate();
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
        return IsFirstPlayer ? Type.Player1Draw : Type.Player2Draw;
    }
}
