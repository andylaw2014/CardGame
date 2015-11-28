public class AttackState : GameState
{
    public AttackState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Attack GamePhase";
    }

    public override void StateCall()
    {
        GameController2.Instance.InitialCombatHandler();
        if (IsYourTurn())
            GameController2.Instance.Player.ToggleAttackableEffect(true);
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
        GameController2.Instance.Player.AddAttackors();
        GameController2.Instance.Player.ToggleAttackableEffect(false);
        GameController2.Instance.Combat.SumbitAttackor();
    }
}