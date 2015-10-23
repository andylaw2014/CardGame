public abstract class GameState
{
    public enum Type
    {
        Player1Reset, Player1Draw, Player1Main, Player1Atk, Player1Def, Player1AtkCot, Player1DefCot, Player1SecMain,
        Player2Reset, Player2Draw, Player2Main, Player2Atk, Player2Def, Player2AtkCot, Player2DefCot, Player2SecMain
    }

    public string StateText { get; protected set; }
    protected readonly bool IsFirstPlayer;

    protected GameState(bool isFirstPlayer)
    {
        IsFirstPlayer = isFirstPlayer;
        StateText = IsYourTurn() ? "Your" : "Opponent";
    }

    public bool IsYourTurn()
    {
        return IsFirstPlayer == GameController.Instance.IsFirstPlayer;
    }

    public virtual void StateCall()
    {
        
    }

    public virtual bool NextPhaseClickable()
    {
        return false;
    }

    public virtual bool AllowPlayCard()
    {
        return false;
    }

    public virtual void EndStateCall()
    {
        
    }

    public abstract Type NextState();
}
