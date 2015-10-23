public class DefenceCounterState : GameState
{
    public DefenceCounterState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Defence Counter Phase";
    }

    public override bool NextPhaseClickable()
    {
        return !IsYourTurn();
    }

    public override bool AllowPlayCard()
    {
        return !IsYourTurn();
    }

    public override void EndStateCall()
    {
        var combat = GameController.Instance.Combat;
        var target = IsYourTurn() ? GameController.Instance.Opponent : GameController.Instance.Player;
        var attacter = IsYourTurn() ? GameController.Instance.Player : GameController.Instance.Opponent;

            foreach (var id in combat.SelectAttackSet)
            {
                var card = attacter.FindCardControllerByIdInBoard(id);
                target.Stats.Hp -= card.Attack;
            }

    }

    public override Type NextState()
    {
        return IsFirstPlayer ? Type.Player1SecMain : Type.Player2SecMain;
    }
}
