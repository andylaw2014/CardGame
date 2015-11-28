public class DefenceState : GameState
{
    public DefenceState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Defence GamePhase";
    }

    public override void StateCall()
    {
        if (IsYourTurn()) return;
        GameController2.Instance.Player.ToggleDefencableEffect(true);
        GameController2.Instance.Opponent.ToggleAttackingEffect(true);
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
        GameController2.Instance.Player.ToggleDefencableEffect(false);
        GameController2.Instance.Opponent.ToggleAttackingEffect(false);
        GameController2.Instance.Opponent.ToggleAttackableEffect(false);
    }
}