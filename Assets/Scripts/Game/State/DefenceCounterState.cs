public class DefenceCounterState : GameState
{
    public DefenceCounterState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Defence Counter GamePhase";
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
        var combat = GameController2.Instance.Combat;
        var target = IsYourTurn() ? GameController2.Instance.Opponent : GameController2.Instance.Player;
        var attacter = IsYourTurn() ? GameController2.Instance.Player : GameController2.Instance.Opponent;

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