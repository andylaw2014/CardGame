public class DefenceState : GameState
{
    public DefenceState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Defence Phase";
    }

    public override void StateCall()
    {
        if (IsYourTurn()) return;
        GameController.Instance.Player.ToggleDefencableEffect(true);
        GameController.Instance.Opponent.ToggleAttackingEffect(true);
    }

    public override bool NextPhaseClickable()
    {
        return !IsYourTurn();
    }

    public override Type NextState()
    {
        return IsFirstPlayer ? Type.Player1AtkCot : Type.Player2AtkCot;
    }

    public override void EndStateCall()
    {
        if (IsYourTurn()) return;
        GameController.Instance.Player.ToggleDefencableEffect(false);
        GameController.Instance.Opponent.ToggleAttackingEffect(false);
        GameController.Instance.Opponent.ToggleAttackableEffect(false);
    }
}
