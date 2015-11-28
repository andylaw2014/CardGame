public class ResetState : GameState
{
    public ResetState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Reset GamePhase";
    }

    public override void StateCall()
    {
        if (IsYourTurn() && !GameController2.Instance.Player.IsResourceAllFull())
            GameController2.Instance.ResourcePanel.Activate();
    }

    public override bool NextPhaseClickable()
    {
        return IsYourTurn();
    }

    public override Type NextState()
    {
        return IsFirstPlayer ? Type.Player1Draw : Type.Player2Draw;
    }
}