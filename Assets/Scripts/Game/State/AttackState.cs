public class AttackState : GameState
{
    public AttackState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Attack Phase";
    }

    public override void StateCall()
    {
        GameController.Instance.InitialCombatHandler();
        if (IsYourTurn())
            GameController.Instance.Player.ToggleAttackableEffect(true);
    }

    public override bool NextPhaseClickable()
    {
        return IsYourTurn();
    }

    public override Type NextState()
    {
        return IsFirstPlayer ? Type.Player1Def : Type.Player2Def;
    }

    public override void EndStateCall()
    {
        if (!IsYourTurn()) return;
        GameController.Instance.Player.AddAttackors();
        GameController.Instance.Player.ToggleAttackableEffect(false);
        GameController.Instance.Combat.SumbitAttackor();
    }
}
