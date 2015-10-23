public class DrawState : GameState
{
    public DrawState(bool isFirstPlayer) : base(isFirstPlayer)
    {
        StateText += " Draw Phase";
    }

    public override bool NextPhaseClickable()
    {
        return IsYourTurn();
    }
    
    public override Type NextState()
    {
        return IsFirstPlayer ? Type.Player1Main : Type.Player2Main;
    }
}
